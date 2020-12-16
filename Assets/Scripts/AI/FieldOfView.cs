using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	[SerializeField] private VariableFloat viewAngle;
	[SerializeField] private VariableFloat viewRadius;
	[SerializeField] private LayerMask targetMask;
	[SerializeField] private LayerMask obstacleMask;
	[SerializeField] private List<Transform> visibleTargets = new List<Transform>();

	public VariableFloat ViewAngle { get => viewAngle; set => viewAngle = value; }
	public VariableFloat ViewRadius { get => viewRadius; set => viewRadius = value; }
	public List<Transform> VisibleTargets { get => visibleTargets; set => visibleTargets = value; }

	private void Start()
	{
		StartCoroutine(FindTargetsWithDelay(0.2f));
	}

	public IEnumerator FindTargetsWithDelay(float delay)
	{
		while(true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	void FindVisibleTargets()
	{
		visibleTargets.Clear();

		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius.Value, targetMask);

		for(int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle.Value / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					visibleTargets.Add(target);
				}
			}
		}
	}

	public Vector3 DirFromAngle(VariableFloat angleInDegrees, bool angleIsGlobal)
	{
		if(!angleIsGlobal)
			angleInDegrees.Value += transform.eulerAngles.y;

		return new Vector3(Mathf.Sin(angleInDegrees.Value * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees.Value * Mathf.Deg2Rad));
	}
	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if(!angleIsGlobal)
			angleInDegrees += transform.eulerAngles.y;

		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
