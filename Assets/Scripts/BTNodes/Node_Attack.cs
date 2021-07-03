using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_Attack : BTBaseNode
{
	private float attackDistance = default;
	private NavMeshAgent navAgent = default;
	private VariableGameObject target;

	public Node_Attack(float attackDistance, NavMeshAgent navAgent, VariableGameObject target)
	{
		this.attackDistance = attackDistance;
		this.navAgent = navAgent;
		this.target = target;
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
				if(t.GetComponent<Player>() != null)
				{
					closestTarget = t;
					dist = Vector3.Distance(navAgent.transform.position, t.position);
				}
			}
		}
		if(closestTarget)
		{
			// Closest target found.
			// Continue with expected behaviour.
			Debug.Log(navAgent.name + " attacked " + closestTarget.name);
			target.Value = null;
			closestTarget.GetComponent<IDamageable>().TakeDamage(navAgent.gameObject, 1);

			status = TaskStatus.Success;
			return status;
		}

		status = TaskStatus.Running;
		return status;
	}
}
