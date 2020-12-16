using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
	[SerializeField] private LayerMask collisionMask;
	[SerializeField] private WaypointsManager waypointsManager;
	[SerializeField] private BTBaseNode tree;
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private Animator animator;
	[SerializeField] private Transform viewTransform;
	[SerializeField] private FieldOfView fov;

	[Header("Variable Floats - GameObjects")]
	[SerializeField] private VariableFloat walkSpeed;
	[SerializeField] private VariableFloat stoppingDistance;

	[SerializeField] private VariableGameObject target;


	private void Awake()
	{
		waypointsManager = FindObjectOfType<WaypointsManager>(); // Not optimal, but it works :)
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		fov = GetComponent<FieldOfView>();

		agent.speed = walkSpeed.Value;
		agent.stoppingDistance = stoppingDistance.Value;
		StartCoroutine(fov.FindTargetsWithDelay(0.2f));
	}

	private void Start()
	{
		Node_Patrol nodePatrol = new Node_Patrol(waypointsManager, agent);
		Node_TargetVisible nodeTargetVisible = new Node_TargetVisible(fov, target);
		Invertor nodeTargetVisibleInvertor = new Invertor(nodeTargetVisible);
		Node_TargetAvailable nodeTargetAvailable = new Node_TargetAvailable(target);
		Invertor nodeTargetAvailableInvertor = new Invertor(nodeTargetAvailable);

		Sequence sequencePatrol = new Sequence(new List<BTBaseNode> { nodeTargetAvailableInvertor, nodePatrol, nodeTargetVisibleInvertor }, "Patrol Sequence");

		tree = new Selector(new List<BTBaseNode> { sequencePatrol });
	}

	private void FixedUpdate()
	{
		tree?.Run();
	}
}
