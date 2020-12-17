using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Selector is responsible for checking all the children their status.
/// </summary>
public class Selector : BTBaseNode
{
	private List<BTBaseNode> children = new List<BTBaseNode>();
	public List<BTBaseNode> Children { get => children; set => children = value; }

	public Selector(List<BTBaseNode> children)
	{
		this.children = children;
	}

	public override TaskStatus Run()
	{
		foreach(BTBaseNode node in children)
		{
			switch(node.Run())
			{
				case TaskStatus.Running:
					status = TaskStatus.Running;
					return status;

				case TaskStatus.Success:
					status = TaskStatus.Success;
					return status;

				// One might fail, but we still need to check the rest if they succeed.
				case TaskStatus.Failed:
					continue;

				default:
					break;
			}
		}

		status = TaskStatus.Failed;
		return status;
	}
}
