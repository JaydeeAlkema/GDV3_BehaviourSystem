using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_AcquireWeapon : BTBaseNode
{
	private Transform target;
	private NavMeshAgent navAgent;
	private float interactionDistance;
	private VariableBool weaponAcquired;

	public Node_AcquireWeapon(Transform target, NavMeshAgent navAgent, float interactionDistance, VariableBool weaponAcquired)
	{
		this.target = target;
		this.navAgent = navAgent;
		this.interactionDistance = interactionDistance;
		this.weaponAcquired = weaponAcquired;
	}

	public override TaskStatus Run()
	{
		if(Vector3.Distance(navAgent.transform.position, target.position) < interactionDistance)
		{
			Debug.Log("Weapon Acquired!");
			weaponAcquired.Value = true;
			status = TaskStatus.Success;
			return status;
		}
		else
		{
			Debug.Log("Acquiring weapon!");
			status = TaskStatus.Running;
			return status;
		}
	}
}
