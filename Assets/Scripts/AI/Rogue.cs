using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{
	[SerializeField] private LayerMask enemyLayermask;
	private Selector tree;
	private NavMeshAgent agent;
	private Animator animator;
	private VariableGameObject target;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
		target = (VariableGameObject)ScriptableObject.CreateInstance("VariableGameObject");
		target.Value = GameObject.FindGameObjectWithTag("Player");

		// Follow Player
		Node_IsEnemyActive node_IsEnemyActive = new Node_IsEnemyActive(50, enemyLayermask, transform);
		Invertor node_IsEnemyActiveInvertor = new Invertor(node_IsEnemyActive);
		Node_Chase node_Chase = new Node_Chase(2, 100, target, agent, false);

		Sequence sequence_FollowPlayer = new Sequence(new List<BTBaseNode> { node_IsEnemyActiveInvertor, node_Chase }, "Ninja Sequence: Follow Player");

		// Throw smoke at Guard
		Node_MoveToTransform node_MoveToTransform = new Node_MoveToTransform(GameObject.FindGameObjectWithTag("Cover").transform, agent, 1f);
		Node_ThrowSmoke node_ThrowSmoke = new Node_ThrowSmoke(enemyLayermask, 5f, transform, 10f);

		Sequence sequence_ThrowSmoke = new Sequence(new List<BTBaseNode> { node_IsEnemyActive, node_MoveToTransform, node_ThrowSmoke }, "Throw Smoke");


		tree = new Selector(new List<BTBaseNode> { sequence_ThrowSmoke, sequence_FollowPlayer });

		if(Application.isEditor)
		{
			gameObject.AddComponent<ShowNodeTreeStatus>().AddConstructor(transform, tree);
		}
	}

	private void FixedUpdate()
	{
		tree?.Run();
	}
}
