using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : AIBehaviour
{
	[SerializeField] [Range(0, 10)] private float min_TimeToWaitAtWaypoint = 0;
	[SerializeField] [Range(0, 10)] private float max_TimeToWaitAtWaypoint = 0;
	[SerializeField] private WaypointsManager waypointsManager;
	[SerializeField] private NavMeshAgent navMeshAgent;

	private Agent agent;
	private bool waiting = false;

	public override void OnEnter()
	{
		base.OnEnter();
		Debug.Log("Patroling!");

		agent = GetComponent<Agent>();
		navMeshAgent.destination = agent.Target.position;
		agent.FloatingStateText.text = "Patroling";
	}

	public override void Execute()
	{
		if(!agent.Target || Vector3.Distance(agent.transform.position, agent.Target.position) < 1)
		{
			if(!waiting)
			{
				StartCoroutine(GetNextWaypoint());
			}
		}
	}

	private IEnumerator GetNextWaypoint()
	{
		waiting = true;
		float waitTime = Random.Range(min_TimeToWaitAtWaypoint, max_TimeToWaitAtWaypoint);
		agent.FloatingStateText.text = "Waiting at Waypoint";
		Debug.Log("Waiting at Waypoint for: " + waitTime.ToString("F2") + "s");
		yield return new WaitForSeconds(waitTime);

		agent.Target = waypointsManager.GetWaypoint();
		navMeshAgent.destination = agent.Target.position;
		Debug.Log("Waiting at Waypoint Ended!");

		waiting = false;
		yield return null;
	}
}
