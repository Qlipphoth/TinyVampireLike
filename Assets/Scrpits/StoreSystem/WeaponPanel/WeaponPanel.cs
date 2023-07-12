using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPanel : Singleton<WeaponPanel>
{
    [SerializeField] List<GameObject> weaponPanelGrids;

    [Header("Title")]
    [SerializeField] TMP_Text title;

    [Header("Mask")]
    [SerializeField] GameObject mask;

    int i, weaponCount;

    private void OnEnable() {
        RefreshWeaponPanel();
    }

    public void RefreshWeaponPanel() {
        ClearWeaponPanel();
        weaponCount = GameManager.Instance.playerWeapons.Count;
        title.text = $"Weapon ({weaponCount}/6)";
        for (i = 0; i < weaponCount; i++) {
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
