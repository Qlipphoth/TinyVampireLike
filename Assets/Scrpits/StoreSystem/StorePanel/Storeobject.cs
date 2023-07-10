using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店中售卖所有物品的基类
/// </summary>
public abstract class StoreObject : MonoBehaviour
{
    public abstract bool isWeapon { get; }
    public abstract void Buy();

}
