using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_IsEnemyActive : BTBaseNode
{
	private float detectionRadius;
	private LayerMask detectionLayer;
	private Transform origin;

	public Node_IsEnemyActive(float detectionRadius, LayerMask detectionLayer, Transform origin)
	{
		this.detectionRadius = detectionRadius;
		this.detectionLayer = detectionLayer;
		this.origin = origin;
	}

	public override TaskStatus Run()
	{
		Collider[] enemiesInDetectionRadius = Physics.OverlapSphere(origin.position, detectionRadius, detectionLayer);
		foreach(Collider enemy in enemiesInDetectionRadius)
		{
			if(enemy.GetComponent<Guard>() != null && enemy.GetComponent<Guard>().active.Value == true)
			{
				Debug.Log("Enemy is Active!");
				status = TaskStatus.Success;
				return status;

			}
		}

		status = TaskStatus.Failed;
		return status;
	}
}
