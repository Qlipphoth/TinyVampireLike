using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDEXPBar : StatesBar
{
    [SerializeField] TMP_Text text;

    public override void Initialize(float currentValue, float maxValue) {
        base.Initialize(currentValue, maxValue);
        SetLevel();
    }

    public override void UpdateStates(float currentValue, float maxValue) {
        if (currentValue >= maxValue) {
            currentValue -= maxValue;
            GameManager.Instance.LevelUp();  // 调用经验系统的升级方法
            SetLevel();
            EmptyStates();  // 重置经验条
        }
        base.UpdateStates(currentValue, maxValue);
    }

    public void SetLevel() {
        text.text = $"Lv {PlayerAttr.Instance.Level}";
    }

    // public override void UpdateStates(float currentValue, float maxValue) {
    //     base.UpdateStates(currentValue, maxValue);
    //     if (fillImageFront.fillAmount == 1) {
    //         fillImageFront.fillAmount = 0;
    //         SetLevel(++level);
    //     }
    // }

}
