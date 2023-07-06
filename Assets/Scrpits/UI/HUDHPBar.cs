using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDHPBar : StatesBar
{
    [SerializeField] TMP_Text text;

    public override void Initialize(float currentValue, float maxValue) {
        base.Initialize(currentValue, maxValue);
        text.text = $"{currentValue} / {maxValue}";
    }

    public override void UpdateStates(float currentValue, float maxValue) {
        base.UpdateStates(currentValue, maxValue);
        text.text = $"{currentValue} / {maxValue}";
    }

}
