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
				status = TaskStatus.Failed;
				break;

			case TaskStatus.Failed:
				status = TaskStatus.Success;
				break;

			case TaskStatus.Running:
				status = TaskStatus.Running;
				break;

			default:
				break;
		}

		return status;
	}
}
