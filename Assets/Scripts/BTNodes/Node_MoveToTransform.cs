using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_MoveToTransform : BTBaseNode
{
	private Transform targetTransform;
	private NavMeshAgent navAgent;
	private float stoppingDistance;

	public Node_MoveToTransform(Transform targetTransform, NavMeshAgent navAgent, float stoppingDistance)
	{
		this.targetTransform = targetTransform;
		this.navAgent = navAgent;
		this.stoppingDistance = stoppingDistance;
	}

	public override TaskStatus Run()
	{
		navAgent.SetDestination(targetTransform.position);
		if(Vector3.Distance(navAgent.transform.position, targetTransform.position) < stoppingDistance)
		{
			status = TaskStatus.Success;
			return status;
		}

		Debug.Log("Moving towards " + targetTransform.name);
		status = TaskStatus.Running;
		return status;
	}
}
