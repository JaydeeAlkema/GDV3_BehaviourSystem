using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
	private WaypointsManager waypointsManager;
	private BTBaseNode tree;
	private NavMeshAgent agent;
	private Animator animator;

	public VariableGameObject target;

	private void Awake()
	{
		waypointsManager = FindObjectOfType<WaypointsManager>(); // Not optimal, but it works :)
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
		target = (VariableGameObject)ScriptableObject.CreateInstance("VariableGameobject");

		Node_Patrol nodePatrol = new Node_Patrol(waypointsManager, agent);

	}

	private void FixedUpdate()
	{
		tree?.Run();
	}

	//private void OnDrawGizmos()
	//{
	//    Gizmos.color = Color.yellow;
	//    Handles.color = Color.yellow;
	//    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
	//    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

	//    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
	//    Gizmos.DrawLine(viewTransform.position, endPointLeft);
	//    Gizmos.DrawLine(viewTransform.position, endPointRight);

	//}
}
