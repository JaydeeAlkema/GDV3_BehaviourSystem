using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatBehaviour : AIBehaviour
{
	private Agent agent;

	public override void OnEnter()
	{
		base.OnEnter();
		agent = GetComponent<Agent>();

		Debug.Log("Retreating!");
		agent.FloatingStateText.text = "Retreating!";
	}

	public override void Execute()
	{

	}
}
