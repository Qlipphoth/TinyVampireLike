using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Store System/Weapon")]
public class WeaponData : ScriptableObject
{
    public Sprite weaponSprite;
    public string weaponName;
    public WeaponCls weaponCls;
    public int weaponDamage;
    public int weaponKnockback;
    public int weaponPierce;
    public int weaponRange;
    [TextArea] public string specialInfo;
}
