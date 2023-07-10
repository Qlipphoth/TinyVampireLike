using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemAttrClause : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TMP_Text value;
    [SerializeField] TMP_Text attrName;

    [Header("Color")]
    [SerializeField] Color buffColor;
    [SerializeField] Color debuffColor;

    public void SetAttrClause(ItemEffect effect) {
        string sign;
        sign = effect.value > 0 ? "+" : "";
        value.color = effect.value > 0 ? buffColor : debuffColor;
        value.text = sign + effect.value.ToString();
        attrName.text = EnumAttrs.getPlayerAttrKey(effect.attr);
    }

}
