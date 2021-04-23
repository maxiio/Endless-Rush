using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
	public static event EventHandler<float> AmountOfDicreasedHealth;

	public override void Hit(float damage) {
		base.Hit(damage);
		Debug.Log("Hit");
		AmountOfDicreasedHealth?.Invoke(this, damage);
	}
}
