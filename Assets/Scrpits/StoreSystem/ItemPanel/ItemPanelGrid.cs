using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemPanelGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image background;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemNum;
    [SerializeField] ItemPanelGridInfo itemPanelGridInfo;

    public void SetGrid(StoreItemBase item, int num) {
        background.color = GameManager.Instance.bgColors[item.itemData.itemLevel];
        itemImage.sprite = item.itemData.itemSprite;
        itemNum.text = num.ToString();
        itemPanelGridInfo.SetItemGridInfo(item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemPanelGridInfo.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemPanelGridInfo.gameObject.SetActive(false);
    }

}
