using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Similair to the Selector. But instead all children need to succeed or the whole sequence will be stopped.
/// </summary>
public class Sequence : BTBaseNode
{
	public string Name { get; set; }
	private List<BTBaseNode> children = new List<BTBaseNode>();
	public List<BTBaseNode> Children { get => children; set => children = value; }

	public Sequence( List<BTBaseNode> children, string name )
	{
		this.children = children;
		this.Name = name;
	}


	public override TaskStatus Run()
	{
		bool childRunning = false;

		foreach( BTBaseNode node in children )
		{
			switch( node.Run() )
			{
				case TaskStatus.Running:
					Debug.Log( Name + " is running!" );
					childRunning = true;
					continue;

				case TaskStatus.Success:
					Debug.Log( Name + " Succeeded!" );
					continue;

				case TaskStatus.Failed:
					Debug.Log( Name + " Failed!" );
					status = TaskStatus.Failed;
					return status;

				default:
					status = TaskStatus.Success;
					return status;
			}
		}
		status = childRunning ? TaskStatus.Running : TaskStatus.Success;
		return status;
	}
}
