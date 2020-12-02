using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : AIBehaviour
{
	private Agent agent;

	public override void OnEnter()
	{
		base.OnEnter();
		agent = GetComponent<Agent>();

		Debug.Log("Attack behaviour!");
		agent.FloatingStateText.text = "Patroling";
	}

	public override void Execute()
	{

	}
}
