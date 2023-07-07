using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Data System/Item")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public EnumAttrs.ItemCls itemCls;
    public List<Effect> effects;
    [TextArea] public string specialInfo;
    public int itemPrice;
}