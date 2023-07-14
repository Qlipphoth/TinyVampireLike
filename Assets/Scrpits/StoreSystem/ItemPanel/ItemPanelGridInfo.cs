using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPanelGridInfo : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemCls;
    [SerializeField] ItemAttrs itemAttrs;
    
    StoreItemBase storeItem;

    public void SetItemGridInfo(StoreItemBase item) {
        storeItem = item;
        RefeshGrid();
    }

    void RefeshGrid() {
        itemImage.sprite = storeItem.itemData.itemSprite;
        itemName.text = storeItem.itemData.itemName;
        itemCls.text = EnumAttrs.getItemCls(storeItem.itemData.itemCls);

        itemAttrs.ClearClauses();

        itemAttrs.genClause(storeItem.itemData.effects);
        itemAttrs.genSpecialInfo(storeItem.itemData.specialInfo);
    }

}
