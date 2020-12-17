using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_DistanceToTarget : BTBaseNode
{
	private float minDistanceToTarget;
	private Transform target;
	private NavMeshAgent navAgent;

	public Node_DistanceToTarget(float minDistanceToTarget, Transform target, NavMeshAgent navAgent)
	{
		this.minDistanceToTarget = minDistanceToTarget;
		this.target = target;
		this.navAgent = navAgent;
	}

	public override TaskStatus Run()
	{
		if(Vector3.Distance(navAgent.transform.position, target.position) < minDistanceToTarget)
		{
			status = TaskStatus.Success;
			return status;
		}

		status = TaskStatus.Failed;
		return status;
	}
}
