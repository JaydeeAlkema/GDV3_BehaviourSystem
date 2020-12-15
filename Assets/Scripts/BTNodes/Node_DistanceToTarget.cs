using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_DistanceToTarget : BTBaseNode
{
	private float minDistanceToTarget;
	private Transform target;
	private NavMeshAgent navAgent;

	public Node_DistanceToTarget(float _minDistanceToTarget, Transform _target, NavMeshAgent _navAgent)
	{
		minDistanceToTarget = _minDistanceToTarget;
		target = _target;
		navAgent = _navAgent;
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
