using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_Patrol : BTBaseNode
{
	/// <summary>
	/// Reference to the Waypoints Manager.
	/// We grab a transform position from this waypoints manager to simulate a patrolling behaviour;
	/// </summary>
	WaypointsManager waypointsManager;
	Transform target;
	NavMeshAgent navAgent;

	float minDistanceToTarget = 1f;

	public Node_Patrol(WaypointsManager _waypointsManager, NavMeshAgent _navAgent)
	{
		waypointsManager = _waypointsManager;
		navAgent = _navAgent;
	}

	public override TaskStatus Run()
	{
		// Get a target to walk towards.
		// This is not really a "target", but more of a transform to walk to.
		if(!target)
		{
			target = waypointsManager.GetWaypoint();
			navAgent.SetDestination(target.position);
		}

		float distToTarget = Vector3.Distance(navAgent.transform.position, target.position);
		// check if a path is available to the target
		if(navAgent.pathStatus == NavMeshPathStatus.PathInvalid || navAgent.pathStatus == NavMeshPathStatus.PathPartial)
		{
			Debug.LogWarning(navAgent.name + " Can't find a path to " + target.name);
			status = TaskStatus.Failed;
			return status;
		}

		if(distToTarget <= minDistanceToTarget)
		{
			Debug.Log(navAgent.name + " reached " + target.name);
			status = TaskStatus.Success;
			return status;
		}

		Debug.Log(navAgent.name + " Patrolling...");
		status = TaskStatus.Running;
		return status;
	}
}
