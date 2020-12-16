using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
	private void OnSceneGUI()
	{
		FieldOfView fov = (FieldOfView)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.ViewRadius.Value);
		Vector3 viewAngleA = fov.DirFromAngle(-fov.ViewAngle.Value / 2, false);
		Vector3 viewAngleB = fov.DirFromAngle(fov.ViewAngle.Value / 2, false);

		Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.ViewRadius.Value);
		Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.ViewRadius.Value);

		Handles.color = Color.red;
		foreach(Transform visibleTarget in fov.VisibleTargets)
		{
			Handles.DrawLine(fov.transform.position, visibleTarget.position);
		}
	}
}
