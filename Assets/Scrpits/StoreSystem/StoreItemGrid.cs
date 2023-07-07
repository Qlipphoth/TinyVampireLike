using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemGrid : MonoBehaviour
{
    [SerializeField] StoreItemBase storeItem;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemCls;
    [SerializeField] ItemAttrs itemAttrs;
    [SerializeField] TMP_Text itemPrice;

    private void OnEnable() {
        RefeshGrid();
    }

    public void SetItemGrid(StoreItemBase item) {
        storeItem = item;
        RefeshGrid();
    }

    public void RefeshGrid() {
        itemImage.sprite = storeItem.itemData.itemSprite;
        itemName.text = storeItem.itemData.itemName;
        itemCls.text = EnumAttrs.getItemCls(storeItem.itemData.itemCls);
        itemAttrs.genClauses(storeItem.itemData.effects);
        itemAttrs.genSpecialInfo(storeItem.itemData.specialInfo);
        itemPrice.text = storeItem.itemData.itemPrice.ToString();
    }

}
