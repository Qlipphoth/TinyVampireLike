using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Data System/Item")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public int itemLevel;
    public EnumAttrs.ItemCls itemCls;
    public List<ItemEffect> effects;
    [TextArea] public string specialInfo;
    public int itemPrice;
}
