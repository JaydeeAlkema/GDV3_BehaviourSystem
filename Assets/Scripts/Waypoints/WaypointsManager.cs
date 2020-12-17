using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
	[SerializeField] private Transform waypointsParent;
	[SerializeField] private List<Transform> waypoints = new List<Transform>();

	public Transform WaypointsParent { get => waypointsParent; set => waypointsParent = value; }
	public List<Transform> Waypoints { get => waypoints; set => waypoints = value; }

	private void Awake()
	{
		GetWaypointsFromParent();
	}

	private void GetWaypointsFromParent()
	{
		waypoints.Clear();
		for(int i = 0; i < waypointsParent.transform.childCount; i++)
		{
			waypoints.Add(waypointsParent.transform.GetChild(i));
		}
	}

	/// <summary>
	/// Returns a random waypointy from the list.
	/// </summary>
	/// <returns></returns>
	public Transform GetWaypoint(int waypointIndex)
	{
			return waypoints[waypointIndex];
	}

	private void OnDrawGizmos()
	{
		GetWaypointsFromParent();

		// Draw some gizmos to clarify  where the waypoints are and which waypoints are each others "neighbours".
		// This is done with 2 seperate for loops so the gizmos are drawn ontop of eachother and do not create inconsistencies.
		if(waypoints.Count > 0)
		{
			// Draw spheres at the waypoints positions
			for(int i = 0; i < waypoints.Count; i++)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(waypoints[i].position, 0.25f);
			}
			// Draw lines between the waypoints.
			for(int i = 0; i < waypoints.Count; i++)
			{
				Gizmos.color = Color.green;
				if(i < waypoints.Count - 1) Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
				else Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
			}
		}
	}
}
