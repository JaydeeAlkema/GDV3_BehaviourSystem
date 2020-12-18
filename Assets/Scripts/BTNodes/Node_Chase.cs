using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_Chase : BTBaseNode
{
	/// <summary>
	/// Anything below this and the chase will stop and behaviour will switch. I.E. Attack
	/// </summary>
	private float minDistanceToChase;
	/// <summary>
	/// Anything above this and the chase will stop and behaviour will switch. I.E. PAtroll
	/// </summary>
	private float maxDistanceToChase;
	/// <summary>
	/// How long the agent can keep on chasing the target.
	/// </summary>
	private float chaseTime;
	private float chaseTimer;

	private VariableGameObject target;
	private NavMeshAgent navAgent;

	public Node_Chase(float minDistanceToChase, float maxDistanceToChase, float chaseTime, VariableGameObject target, NavMeshAgent navAgent)
	{
		this.minDistanceToChase = minDistanceToChase;
		this.maxDistanceToChase = maxDistanceToChase;
		this.chaseTime = chaseTime;
		chaseTimer = chaseTime;

		this.target = target;
		this.navAgent = navAgent;
		navAgent.stoppingDistance = minDistanceToChase;
	}

	public override TaskStatus Run()
	{
		// Check if a path is available to the target
		if(navAgent.pathStatus == NavMeshPathStatus.PathInvalid)
		{
			Debug.LogWarning(navAgent.name + " Can't find a path to " + target.name);
			status = TaskStatus.Failed;
			return status;
		}

		// check if Target is not null. Just in case a different guard killed it or an error occured.
		if(!target)
		{
			Debug.Log(navAgent.name + " Lost it's target. (NULL)");
			status = TaskStatus.Failed;
			return status;
		}

		float distToTarget = Vector3.Distance(navAgent.transform.position, target.Value.transform.position);
		navAgent.SetDestination(target.Value.transform.position);

		// Check if target has went further than the max distance to chase.
		// If the target has gone above the max distance, start the chase timer. This is done so the agent still keeps it's interrest in the target.
		// This results in a somewhat more "realistc" behaviour.
		if(distToTarget >= maxDistanceToChase)
		{
			Debug.Log(navAgent.name + " Has lost interrest in " + target.name + "(Too far and Chase Time below 0)");
			target = null;
			status = TaskStatus.Failed;
			return status;
		}

		// Check if agent is close enough to target.
		// If not, Continue on running.
		if(distToTarget <= minDistanceToChase)
		{
			Debug.Log(navAgent.name + " has reached it's target!");
			status = TaskStatus.Success;
			return status;
		}
		else
		{
			Debug.Log(navAgent.name + " Chasing target " + navAgent.name);
			status = TaskStatus.Running;
			return status;
		}
	}
}
