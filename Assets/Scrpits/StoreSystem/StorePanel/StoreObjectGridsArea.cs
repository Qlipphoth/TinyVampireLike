using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreObjectGridsArea : Singleton<StoreObjectGridsArea>
{
    [SerializeField] StoreItemGrid[] storeItemGrids;
    [SerializeField] StoreWeaponGrid[] storeWeaponGrids;
    [SerializeField] int itemGridIdx, weaponGridIdx;

    HorizontalLayoutGroup storeObjectGridsAreaLayout;

    protected override void Awake() {
        base.Awake();
        storeObjectGridsAreaLayout = GetComponent<HorizontalLayoutGroup>();
    }

    public void DeactivateLayout() {
        storeObjectGridsAreaLayout.enabled = false;
    }

    public void RefreshGridsArea(ref List<StoreObject> storeObjects) {
        storeObjectGridsAreaLayout.enabled = true;
        ResetGrids();
        foreach (StoreObject storeObject in storeObjects) {
            if (storeObject.isWeapon) {
                storeWeaponGrids[weaponGridIdx].SetWeaponGrid((StoreWeaponBase)storeObject);
                storeWeaponGrids[weaponGridIdx].gameObject.SetActive(true);
                weaponGridIdx++;
            } else {
                storeItemGrids[itemGridIdx].SetItemGrid((StoreItemBase)storeObject);
                storeItemGrids[itemGridIdx].gameObject.SetActive(true);
                itemGridIdx++;
            }
        }
    }

    private void ResetGrids() {
        foreach (StoreItemGrid storeItemGrid in storeItemGrids) {
            storeItemGrid.gameObject.SetActive(false);
        }
        foreach (StoreWeaponGrid storeWeaponGrid in storeWeaponGrids) {
            storeWeaponGrid.gameObject.SetActive(false);
        }
        itemGridIdx = 0;
        weaponGridIdx = 0;
    }

    public void RefreshBtns() {
        foreach (StoreItemGrid storeItemGrid in storeItemGrids) {
            if (storeItemGrid.gameObject.activeSelf) {
                storeItemGrid.RefreshBtn();
            }
        }
        foreach (StoreWeaponGrid storeWeaponGrid in storeWeaponGrids) {
            if (storeWeaponGrid.gameObject.activeSelf) {
                storeWeaponGrid.RefreshBtn();
            }
        }
    }
    
}
