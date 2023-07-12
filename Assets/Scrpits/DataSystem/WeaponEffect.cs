using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponEffect : Effect
{
    public EnumAttrs.WeaponAttrs attr;
    public List<float> value;
}
