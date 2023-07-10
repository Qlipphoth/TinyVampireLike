using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemBase : StoreObject
{
    [SerializeField] public ItemData itemData;

    public override bool isWeapon => false;

    public override void Buy() {
        getItemBase();
    }

    private void getItemBase() {
        foreach (ItemEffect effect in itemData.effects) {
            PlayerAttr.GetChangePlayerAttrFunc(effect.attr).Invoke(effect.value);
        }
    }

}
