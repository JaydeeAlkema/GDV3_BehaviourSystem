using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_TargetAvailable : BTBaseNode
{
	private VariableGameObject target;

	public Node_TargetAvailable(VariableGameObject target)
	{
		this.target = target;
	}

	public override TaskStatus Run()
	{
		if(target.Value == null || target.Value.gameObject.activeSelf == false)
		{
			Debug.Log("Target is not available!");
			status = TaskStatus.Failed;
		}
		else
		{
			Debug.Log("Target is available!");
			status = TaskStatus.Success;
		}

		return status;
	}
}
