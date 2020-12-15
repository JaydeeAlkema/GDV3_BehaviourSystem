using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Bool : BTBaseNode
{
	private VariableBool value;

	public Node_Bool(VariableBool _value)
	{
		value = _value;
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
			status = TaskStatus.Failed;
			return status;
		}
	}
}
