﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node_TargetVisible : BTBaseNode
{
	private VariableGameObject target;
	private FieldOfView fov;

	public Node_TargetVisible(VariableGameObject target, FieldOfView fov)
	{
		this.target = target;
		this.fov = fov;
	}

	public override TaskStatus Run()
	{
		if(fov.VisibleTargets.Count <= 0)
		{
			Debug.Log("No Targets in sight!");
			target.Value = null;
			status = TaskStatus.Failed;
			return status;
		}
		else
		{
			target.Value = fov.VisibleTargets[0].gameObject; 
			Debug.Log("Target in sight! Targeting " + target.Value.name);
			status = TaskStatus.Success;
			return status;
		}
	}
}