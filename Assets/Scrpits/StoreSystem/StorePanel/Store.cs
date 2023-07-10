using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : Singleton<Store>
{
    [SerializeField] GameObject storeItemGridPrefab;
    [SerializeField] GameObject storeWeaponGridPrefab;
    [SerializeField] Transform storeObjectGridArea;
    [SerializeField] List<StoreObject> storeObjects;

    HorizontalLayoutGroup storeObjectGridAreaLayout;    
    List<StoreObject> curStoreObjects = new List<StoreObject>();
    GameObject curGrid;

    protected override void Awake() {
        base.Awake();
        storeObjectGridAreaLayout = storeObjectGridArea.gameObject.GetComponent<HorizontalLayoutGroup>();
    }

    private void OnEnable() {
        RefeshStore();
    }
 
    public void DeactivateLayout() {
        storeObjectGridAreaLayout.enabled = false;
    }

    public void RefeshStore(int ItemNum = 4) {
        for (int i = 0; i < ItemNum; i++) {
            curStoreObjects.Add(storeObjects[Random.Range(0, storeObjects.Count)]);
        }
        RefeshGridArea(ref curStoreObjects);
        curStoreObjects.Clear();
    }

    private void RefeshGridArea(ref List<StoreObject> storeObjects) {
        storeObjectGridAreaLayout.enabled = true;
        // Clear all store items
        foreach (Transform child in storeObjectGridArea) {
            Destroy(child.gameObject);
        }
        // Generate store items
        foreach (StoreObject storeObject in storeObjects) {
            if (storeObject.isWeapon) {
                curGrid = Instantiate(storeWeaponGridPrefab, storeObjectGridArea);
                curGrid.GetComponent<StoreWeaponGrid>().SetWeaponGrid((StoreWeaponBase)storeObject);
            }
            else {
                curGrid = Instantiate(storeItemGridPrefab, storeObjectGridArea);
                curGrid.GetComponent<StoreItemGrid>().SetItemGrid((StoreItemBase)storeObject);
            }
        }
    }
}
