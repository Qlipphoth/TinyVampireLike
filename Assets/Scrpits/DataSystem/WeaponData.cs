using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Data System/Weapon")]
public class WeaponData : ScriptableObject
{
    public Sprite weaponSprite;
    public string weaponName;
    public EnumAttrs.WeaponCls weaponCls;
    public WeaponEffect damage;
    public WeaponEffect fireRate;
    public WeaponEffect range;
    public List<WeaponEffect> otherEffects;
    public int weaponPrice;
    [TextArea] public string specialInfo;
    
}
