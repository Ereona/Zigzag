﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Events/Int Event Channel")]
public class IntEventChannelSO : ScriptableObject
{
	public UnityAction<int> OnEventRaised;

	public void RaiseEvent(int value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
