using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponAttrClause : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TMP_Text attrName;
    [SerializeField] TMP_Text value;

    public void SetAttrClause(WeaponEffect effect, int weaponLevel) {
        attrName.text = EnumAttrs.getWeaponAttrKey(effect.attr) + ": ";
        value.text = effect.value[weaponLevel].ToString();
    }
}
