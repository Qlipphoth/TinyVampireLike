using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Store System/Item")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public ItemCls itemCls;
    public int weaponDamage;
    public int weaponKnockback;
    public int weaponPierce;
    public int weaponRange;
    [TextArea] public string specialInfo;
}
