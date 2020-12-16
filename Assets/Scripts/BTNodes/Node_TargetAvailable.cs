using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_TargetAvailable : BTBaseNode
{
	private VariableGameObject target;

	public Node_TargetAvailable(VariableGameObject _target)
	{
		target = _target;
	}

	public override TaskStatus Run()
	{
		if(target.Value == null || target.Value.gameObject.activeSelf == false || target.Value.gameObject.activeInHierarchy == false)
		{
			Debug.Log("Target is not available!");
			status = TaskStatus.Failed;
			return status;
		}

		Debug.Log("Target is available!");
		status = TaskStatus.Success;
		return status;
	}
}
