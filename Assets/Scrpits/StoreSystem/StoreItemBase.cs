using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemBase : MonoBehaviour
{
    [SerializeField] public ItemData itemData;

    public void getItem() {
        foreach (Effect effect in itemData.effects) {
            PlayerAttr.ChangePlayerAttrDict[(int)effect.attr].Invoke(effect.value);
        }
    }

}
