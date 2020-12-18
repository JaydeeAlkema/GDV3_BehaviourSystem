using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_ThrowSmoke : BTBaseNode
{
	private LayerMask enemyLayer;
	private float throwRange;
	private Transform origin;

	float smokeCooldown;
	float cooldownTimer = 0;

	public Node_ThrowSmoke(LayerMask enemyLayer, float throwRange, Transform origin, float smokeCooldown)
	{
		this.enemyLayer = enemyLayer;
		this.throwRange = throwRange;
		this.origin = origin;
		this.smokeCooldown = smokeCooldown;
	}

	public override TaskStatus Run()
	{
		cooldownTimer -= Time.fixedDeltaTime;

		if(cooldownTimer <= 0)
		{
			Collider[] enemiesInRange = Physics.OverlapSphere(origin.position, throwRange, enemyLayer);
			foreach(Collider enemy in enemiesInRange)
			{
				if(enemy.GetComponent<Guard>().isSmoked == false)
				{
					enemy.GetComponent<Guard>().TriggerSmoke();
					Debug.Log("Smoked " + enemy.transform.name);
				}
			}

			cooldownTimer = smokeCooldown;
			status = TaskStatus.Success;
			return status;
		}

		status = TaskStatus.Running;
		return status;
	}
}
