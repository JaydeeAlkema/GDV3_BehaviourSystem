using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_TargetVisible : BTBaseNode
{
	private VariableGameObject target;
	private FieldOfView fov;

	public Node_TargetVisible(FieldOfView _fov, VariableGameObject _target)
	{
		fov = _fov;
		target = _target;
	}

	public override TaskStatus Run()
	{
		if(fov.VisibleTargets.Count <= 0)
		{
			status = TaskStatus.Failed;
			return status;
		}
		else
		{
			target.Value = fov.VisibleTargets[0].gameObject;
			status = TaskStatus.Success;
			return status;
		}
	}
}
