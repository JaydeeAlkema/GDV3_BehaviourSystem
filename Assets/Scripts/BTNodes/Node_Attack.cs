﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_Attack : BTBaseNode
{
	/// <summary>
	/// The Distance a target has to be in.
	/// </summary>
	private float attackDistance = default;
	/// <summary>
	/// NavMeshAgent Reference.
	/// </summary>
	private NavMeshAgent navAgent = default;
	/// <summary>
	/// VariableGameobject Reference.
	/// </summary>
	private VariableGameObject target;

	public Node_Attack(float _attackDistance, NavMeshAgent _navAgent, VariableGameObject _target)
	{
		attackDistance = _attackDistance;
		navAgent = _navAgent;
		target = _target;
	}

	public override TaskStatus Run()
	{
		// Get all colliders in attack range.
		Collider[] collidersInRange = Physics.OverlapSphere(navAgent.transform.position, attackDistance);
		if(collidersInRange.Length == 0)
		{
			status = TaskStatus.Failed;
			return status;
		}

		// Filter Array of colliders. Remove all NON IDamageables. Add leftover to new list
		List<Transform> targets = new List<Transform>();
		foreach(Collider collider in collidersInRange)
		{
			if(collider.GetComponent<IDamageable>() != null) targets.Add(collider.transform);
		}
		if(targets.Count == 0)
		{
			status = TaskStatus.Failed;
			return status;
		}

		// Now check for closest target.
		Transform closestTarget = targets[0];
		float dist = Mathf.Infinity;    // begin from furthest possible point.
		foreach(Transform t in closestTarget)
		{
			if(Vector3.Distance(navAgent.transform.position, t.position) < dist)
			{
				closestTarget = t;
				dist = Vector3.Distance(navAgent.transform.position, t.position);
			}
		}

		// Closest target found.
		// Continue with expected behaviour.
		Debug.LogError(navAgent.name + " attacked " + closestTarget.name);
		target.Value = null;
		closestTarget.GetComponent<IDamageable>().TakeDamage(navAgent.gameObject, 1);
		status = TaskStatus.Success;
		return status;
	}
}