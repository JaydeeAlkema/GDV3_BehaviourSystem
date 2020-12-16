using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertor : BTBaseNode
{
	private BTBaseNode child;

	public Invertor(BTBaseNode _child)
	{
		child = _child;
	}

	public override TaskStatus Run()
	{
		switch(child.Run())
		{
			case TaskStatus.Success:
				status = TaskStatus.Running;
				break;

			case TaskStatus.Failed:
				status = TaskStatus.Success;
				break;

			case TaskStatus.Running:
				status = TaskStatus.Failed;
				break;

			default:
				break;
		}

		return status;
	}
}
