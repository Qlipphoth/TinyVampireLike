using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemBase : MonoBehaviour
{
    [SerializeField] public ItemData itemData;

    public void BuyItem() {
        getItemBase();
    }

    private void getItemBase() {
        foreach (Effect effect in itemData.effects) {
            PlayerAttr.GetChangePlayerAttrFunc(effect.attr).Invoke(effect.value);
        }
    }

}
