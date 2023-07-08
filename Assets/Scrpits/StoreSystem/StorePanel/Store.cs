using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : Singleton<Store>
{
    [SerializeField] GameObject storeItemGridPrefab;
    [SerializeField] Transform storeItemGridArea;
    [SerializeField] List<StoreItemBase> storeItems;

    HorizontalLayoutGroup storeItemGridAreaLayout;    
    List<StoreItemBase> curStoreItems = new List<StoreItemBase>();

    protected override void Awake() {
        base.Awake();
        storeItemGridAreaLayout = storeItemGridArea.gameObject.GetComponent<HorizontalLayoutGroup>();
    }

    private void OnEnable() {
        RefeshStore();
    }
 
    public void DeactivateLayout() {
        storeItemGridAreaLayout.enabled = false;
    }

    public void RefeshStore(int ItemNum = 4) {
        for (int i = 0; i < ItemNum; i++) {
            curStoreItems.Add(storeItems[Random.Range(0, storeItems.Count)]);
        }
        RefeshGridArea(ref curStoreItems);
        curStoreItems.Clear();
    }

    private void RefeshGridArea(ref List<StoreItemBase> storeItems) {
        storeItemGridAreaLayout.enabled = true;
        // Clear all store items
        foreach (Transform child in storeItemGridArea) {
            Destroy(child.gameObject);
        }
        // Generate store items
        foreach (StoreItemBase storeItem in storeItems) {
            GameObject itemGrid = Instantiate(storeItemGridPrefab, storeItemGridArea);
            itemGrid.GetComponent<StoreItemGrid>().SetItemGrid(storeItem);
        }
    }
}
