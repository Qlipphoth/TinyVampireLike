using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreWeaponBase : StoreObject
{
    public WeaponData weaponData;
    public GameObject weaponPrefab;

    public override bool isWeapon => true;
    public int weaponLevel;

    public override void Buy() {
        if (GameManager.Instance.playerWeapons.Count != 6) getWeaponBase();
        WeaponPanel.Instance.RefreshWeaponPanel();
    }

    private void getWeaponBase() {
        GameManager.Instance.playerWeapons.Add(Instantiate(this, GameManager.Instance.transform));
    }

}
