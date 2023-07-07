using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Singleton<Store>
{
    [SerializeField] GameObject storeItemGridPrefab;
    [SerializeField] Transform storeItemGridParent;
    [SerializeField] List<StoreItemBase> storeItems;

    List<StoreItemBase> curStoreItems = new List<StoreItemBase>();

    private void OnEnable() {
        RefeshStore();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            RefeshStore();
        }
    }

    /// <summary>
    /// Refesh store with 4 items
    /// </summary>
    public void RefeshStore() {
        for (int i = 0; i < 4; i++) {
            curStoreItems.Add(storeItems[Random.Range(0, storeItems.Count)]);
        }
        RefeshGridArea(ref curStoreItems);
        curStoreItems.Clear();
    }

    /// <summary>
    /// Refesh store with ItemNum items
    /// </summary>
    /// <param name="ItemNum"></param>
    public void RefeshStore(int ItemNum) {
        for (int i = 0; i < ItemNum; i++) {
            curStoreItems.Add(storeItems[Random.Range(0, storeItems.Count)]);
        }
        RefeshGridArea(ref curStoreItems);
        curStoreItems.Clear();
    }

    /// <summary>
    /// Refesh grid area with store items
    /// </summary>
    /// <param name="storeItems"></param>
    public void RefeshGridArea(ref List<StoreItemBase> storeItems) {
        // Clear all store items
        foreach (Transform child in storeItemGridParent) {
            Destroy(child.gameObject);
        }
        // Generate store items
        foreach (StoreItemBase storeItem in storeItems) {
            GameObject itemGrid = Instantiate(storeItemGridPrefab, storeItemGridParent);
            itemGrid.GetComponent<StoreItemGrid>().SetItemGrid(storeItem);
        }
    }
}
