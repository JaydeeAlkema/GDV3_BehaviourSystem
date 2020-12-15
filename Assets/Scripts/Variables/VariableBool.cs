using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VariableFloat_", menuName = "Variables/VariableBool")]
public class VariableBool : BaseScriptableObject
{
	//Old value, New value
	public System.Action<bool, bool> OnValueChanged;
	[SerializeField] private bool value;
	public bool Value
	{
		get { return value; }
		set
		{
			OnValueChanged?.Invoke(this.value, value);
			this.value = value;
		}
	}
}
