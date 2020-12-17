using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Bool : BTBaseNode
{
	private VariableBool value;

	public Node_Bool(VariableBool value)
	{
		this.value = value;
	}

	public Node_Bool(bool value)
	{
		this.value.Value = value;
	}

	public override TaskStatus Run()
	{
		if(value.Value == true)
		{
			status = TaskStatus.Success;
			return status;
		}
		else
		{
			Debug.Log(value.name + " Check Failed!");
			status = TaskStatus.Failed;
			return status;
		}
	}
}
