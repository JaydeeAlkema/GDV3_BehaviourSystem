using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Similair to the Selector. But instead all children need to succeed or the whole sequence will be stopped.
/// </summary>
public class Sequence : BTBaseNode
{
	private string name;
	private List<BTBaseNode> children = new List<BTBaseNode>();
	public List<BTBaseNode> Children { get => children; set => children = value; }

	public Sequence(List<BTBaseNode> _children, string _name)
	{
		children = _children;
		name = _name;
	}


	public override TaskStatus Run()
	{
		foreach(BTBaseNode node in children)
		{
			switch(node.Run())
			{
				case TaskStatus.Running:
					Debug.Log(name + " is running!");
					status = TaskStatus.Running;
					return status;

				case TaskStatus.Success:
					continue;

				case TaskStatus.Failed:
					Debug.Log(name + " Failed!");
					status = TaskStatus.Failed;
					return status;

				default:
					break;
			}
		}

		Debug.Log(name + " Succeeded!");
		status = TaskStatus.Success;
		return status;
	}
}
