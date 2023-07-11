using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPanel : Singleton<WeaponPanel>
{
    [SerializeField] Transform weaponPanelGridParent;
    [SerializeField] List<GameObject> weaponPanelGrids;

    [Header("Mask")]
    [SerializeField] GameObject mask;

    private void OnEnable() {
        RefreshWeaponPanel();
    }

    public void RefreshWeaponPanel() {
        ClearWeaponPanel();
        for (int i = 0; i < GameManager.Instance.playerWeapons.Count; i++) {
            weaponPanelGrids[i].GetComponent<WeaponPanelGrid>().SetWeaponGrid(i);
            weaponPanelGrids[i].SetActive(true);
        }
    }

    void ClearWeaponPanel() {
        for (int i = 0; i < weaponPanelGrids.Count; i++) {
            weaponPanelGrids[i].SetActive(false);
        }
    }

    public void SetMaskState(bool state) {
        mask.SetActive(state);
    }

}
