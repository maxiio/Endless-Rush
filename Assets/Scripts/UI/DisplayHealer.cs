using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DisplayHealer : DisplayOnText {
    private Healer _healer;

    protected override void Awake() {
        base.Awake();
        _healer = gameObject.GetComponentInParent<Healer>();
        UpdateText(this, _healer.HealthToHeal);
    }
}
