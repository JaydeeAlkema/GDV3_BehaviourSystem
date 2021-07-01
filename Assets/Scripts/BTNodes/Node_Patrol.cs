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
	NavMeshAgent navAgent;

	private Transform waypointTarget = null;
	float minDistanceToTarget = 1.15f;
	int waypointIndex = 0;

	public Node_Patrol( WaypointsManager waypointsManager, NavMeshAgent navAgent )
	{
		this.waypointsManager = waypointsManager;
		this.navAgent = navAgent;
		waypointTarget = null;
	}

	public override TaskStatus Run()
	{
		// Get a target to walk towards.
		// This is not really a "target", but more of a transform to walk to.
		if( waypointTarget == null )
		{
			waypointTarget = waypointsManager.Waypoints[0];
			navAgent.SetDestination( waypointTarget.transform.position );
		}

		// check if a path is available to the target
		//if( navAgent.pathStatus == NavMeshPathStatus.PathInvalid || navAgent.pathStatus == NavMeshPathStatus.PathPartial )
		//{
		//	Debug.LogWarning( navAgent.name + " Can't find a path to " + waypointTarget.name );
		//	status = TaskStatus.Failed;
		//	return status;
		//}

		if( waypointTarget != null )
		{
			Debug.Log( navAgent.name + " Patrolling..." );
			navAgent.SetDestination( waypointTarget.transform.position );

			float distToTarget = Vector3.Distance( navAgent.transform.position, waypointTarget.transform.position );
			if( distToTarget <= minDistanceToTarget )
			{
				Debug.Log( navAgent.name + " reached " + waypointTarget.name + ". Walking to new Waypoint!" );
				waypointTarget = GetNextWaypoint();
				navAgent.SetDestination( waypointTarget.transform.position );
			}

			status = TaskStatus.Running;
		}

		return status;
	}
	Transform GetNextWaypoint()
	{
		waypointIndex++;

		if( waypointIndex >= waypointsManager.Waypoints.Count )
			waypointIndex = 0;

		return waypointsManager.GetWaypoint( waypointIndex );
	}
}
