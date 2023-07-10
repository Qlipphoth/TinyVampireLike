using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreWeaponBase : StoreObject
{
    public WeaponData weaponData;
    public GameObject weaponPrefab;

    public override bool isWeapon => true;

    public override void Buy() {
        getWeaponBase();
    }

    private void getWeaponBase() {
        GameManager.Instance.playerGuns.Add(weaponPrefab);
    }

}
