using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_TargetAvailable : BTBaseNode
{
	private VariableGameObject target;
	private VariableBool ownerActive;

	public Node_TargetAvailable(VariableGameObject target)
	{
		this.target = target;
	}

	public Node_TargetAvailable(VariableGameObject target, VariableBool ownerActive)
	{
		this.target = target;
		this.ownerActive = ownerActive;
	}

	public override TaskStatus Run()
	{
		if(target.Value == null || target.Value.gameObject.activeSelf == false)
		{
			Debug.Log("Target is not available!");
			status = TaskStatus.Failed;
			ownerActive.Value = false;
		}
		else
		{
			Debug.Log("Target is available!");
			status = TaskStatus.Success;
			ownerActive.Value = true;
		}

		return status;
	}
}
