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
    [SerializeField] Button buyBtn;

    private void OnEnable() {
        RefeshGrid();
        buyBtn.onClick.AddListener(Buy);
    }

    private void OnDisable() {
        buyBtn.onClick.RemoveListener(Buy);
    }

    public void SetItemGrid(StoreItemBase item) {
        storeItem = item;
        RefeshGrid();
    }

    void RefeshGrid() {
        itemImage.sprite = storeItem.itemData.itemSprite;
        itemName.text = storeItem.itemData.itemName;
        itemCls.text = EnumAttrs.getItemCls(storeItem.itemData.itemCls);
        itemAttrs.genClauses(storeItem.itemData.effects);
        itemAttrs.genSpecialInfo(storeItem.itemData.specialInfo);
        buyBtn.GetComponentInChildren<TMP_Text>().text = storeItem.itemData.itemPrice.ToString();
    }

    public void Buy() {
        storeItem.BuyItem();
        Store.Instance.DeactivateLayout();
        Destroy(gameObject);
        PlayerStatsPanel.Instance.RefreshStatsPanel();
    }

}
