using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
	[SerializeField] private WaypointsManager waypointsManager;
	[SerializeField] private Selector tree;
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private Animator animator;
	[SerializeField] private Transform viewTransform;
	[SerializeField] private FieldOfView fov;

	[Header("Variable ScriptableObjects")]
	[SerializeField] private VariableBool weaponAvailable;

	[SerializeField] private VariableFloat walkSpeed;
	[SerializeField] private VariableFloat stoppingDistance;
	[SerializeField] private VariableFloat attackRange;
	[SerializeField] private VariableFloat viewDistance;

	[SerializeField] private VariableGameObject target;


	private void Awake()
	{
		waypointsManager = FindObjectOfType<WaypointsManager>(); // Not optimal, but it works :)
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		fov = GetComponent<FieldOfView>();

		agent.speed = walkSpeed.Value;
		StartCoroutine(fov.FindTargetsWithDelay(0.2f));
	}

	private void Start()
	{

		target = (VariableGameObject)ScriptableObject.CreateInstance("VariableGameObject");
		weaponAvailable = (VariableBool)ScriptableObject.CreateInstance("VariableBool");

		weaponAvailable.Value = false;

		// Patrol Behaviour
		Node_Patrol node_Patrol = new Node_Patrol(waypointsManager, agent);
		Node_TargetVisible node_TargetVisible = new Node_TargetVisible(agent.transform, target, fov);
		Invertor node_TargetVisibleInvertor = new Invertor(node_TargetVisible);
		Node_TargetAvailable node_TargetAvailable = new Node_TargetAvailable(target);
		Invertor node_TargetAvailableInvertor = new Invertor(node_TargetAvailable);

		Sequence sequencePatrol = new Sequence(new List<BTBaseNode> { node_TargetAvailableInvertor, node_Patrol, node_TargetVisibleInvertor }, "Patrol Sequence");

		// Chase & Attack Behaviour
		Node_Bool node_WeaponAvailable = new Node_Bool(weaponAvailable);
		Node_MoveToTransform node_MoveToTransform = new Node_MoveToTransform(GameObject.FindGameObjectWithTag("Weapon").transform, agent, 2f); // Again... Not optimal, but it works!
		Node_AcquireWeapon node_AcquireWeapon = new Node_AcquireWeapon(GameObject.FindGameObjectWithTag("Weapon").transform, agent, 2f, weaponAvailable);
		Node_Chase node_Chase = new Node_Chase(1, 10, 5f, target, agent);
		Node_Attack node_Attack = new Node_Attack(attackRange.Value, agent, target);

		Sequence sequence_AcquireWeapon = new Sequence(new List<BTBaseNode> { node_MoveToTransform, node_AcquireWeapon }, "Sequence: Acquire Weapon");
		Selector selector_HasWeapon = new Selector(new List<BTBaseNode> { node_WeaponAvailable, sequence_AcquireWeapon });
		Sequence sequence_Chase = new Sequence(new List<BTBaseNode> { node_TargetAvailable, selector_HasWeapon, node_Chase, node_Attack }, "Sequence: Chasing");


		tree = new Selector(new List<BTBaseNode> { sequence_Chase, sequencePatrol });

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
