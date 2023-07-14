using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : Singleton<Store>
{
    [SerializeField] List<StoreObject> storeObjects;

    [Header("Store Gem Bar")]
    [SerializeField] GemBar StoreGemBar;

    [Header("Refresh Button")]
    [SerializeField] ConsumeGemBtn RefreshBtn;
    [SerializeField] int RefreshGemNum = 10;

    List<StoreObject> curStoreObjects = new List<StoreObject>();
    GameObject curGrid;

    private void OnEnable() {
        RefeshStore();
        // StoreGemBar.Initialize();
        RefreshBtn.Initialize(RefreshGemNum);
        RefreshBtn.consumeGemBtn.onClick.AddListener(() => RefreshStoreWithBtn());
    }

    private void OnDisable() {
        RefreshBtn.consumeGemBtn.onClick.RemoveListener(() => RefreshStoreWithBtn());
    }
 
    private void RefeshStore(int ItemNum = 4) {
        for (int i = 0; i < ItemNum; i++) {
            curStoreObjects.Add(storeObjects[Random.Range(0, storeObjects.Count)]);
        }
        StoreObjectGridsArea.Instance.RefreshGridsArea(ref curStoreObjects);
        curStoreObjects.Clear();
    }

    private void RefreshStoreWithBtn() {
        if (PlayerAttr.Instance.GemNum < RefreshGemNum) return;
        PlayerAttr.Instance.GemNum -= RefreshGemNum;
        RefeshStore();
        RefreshGem();
    }

    public void RefreshGem() {
        StoreGemBar.Initialize();
        RefreshBtn.IsGemEnough();
        StoreObjectGridsArea.Instance.RefreshBtns();
    }

}
