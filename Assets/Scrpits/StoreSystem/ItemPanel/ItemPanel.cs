using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : Singleton<ItemPanel>
{
    [SerializeField] List<ItemPanelGrid> itemGrids;

    int i, idx;

    private void OnEnable() {
        RefreshItemPanel();
    }

    public void RefreshItemPanel() {
        ClearItemPanel();
        foreach (KeyValuePair<StoreItemBase, int> item in GameManager.Instance.playerItems) {
            itemGrids[idx].SetGrid(item.Key, item.Value);
            itemGrids[idx].gameObject.SetActive(true);
            idx++;
        }
    }

    void ClearItemPanel() {
        for (i = 0; i < itemGrids.Count; i++) {
            itemGrids[i].gameObject.SetActive(false);
        }
        idx = 0;
    }

}
