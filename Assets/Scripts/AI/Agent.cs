using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour, IDamageable
{
	public AIBehaviourSelector AISelector { get; private set; }
	public BlackBoard BlackBoard { get; private set; }
	public WaypointsManager WaypointsManager { get; private set; }
	public Transform Target { get; set; }
	public TMPro.TextMeshProUGUI FloatingStateText { get; set; }


	// Start is called before the first frame update
	void Start()
	{
		OnInitialize();
	}

	public void OnInitialize()
	{
		BlackBoard = GetComponent<BlackBoard>();
		AISelector = GetComponent<AIBehaviourSelector>();
		WaypointsManager = FindObjectOfType<WaypointsManager>();
		FloatingStateText = GetComponentInChildren<TMPro.TextMeshProUGUI>();

		BlackBoard.OnInitialize();
		AISelector.OnInitialize(BlackBoard);

		Target = WaypointsManager.GetWaypoint();
	}

	// Update is called once per frame
	void Update()
	{
		AISelector.OnUpdate();

		if(Input.GetKeyDown(KeyCode.Space))
		{
			TakeDamage(10);
		}

		var distance = BlackBoard.GetFloatVariableValue(VariableType.Distance);
		distance.Value = transform.position.magnitude;
		AISelector.EvaluateBehaviours();
	}

	public void TakeDamage(float damage)
	{
		FloatValue res = BlackBoard.GetFloatVariableValue(VariableType.Health);
		if(res)
		{
			res.Value -= damage;
		}
	}
}

public interface IDamageable
{
	void TakeDamage(float damage);
}