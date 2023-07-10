using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreWeaponGrid : MonoBehaviour
{
    [SerializeField] StoreWeaponBase weapon;
    [SerializeField] Image weaponImage;
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text weaponCls;
    [SerializeField] WeaponAttrs weaponAttrs;
    [SerializeField] Button buyBtn;

    private void OnEnable() {
        RefeshGrid();
        buyBtn.onClick.AddListener(Buy);
    }

    private void OnDisable() {
        buyBtn.onClick.RemoveListener(Buy);
    }

    public void SetWeaponGrid(StoreWeaponBase weapon) {
        this.weapon = weapon;
        RefeshGrid();
    }

    void RefeshGrid() {
        weaponImage.sprite = weapon.weaponData.weaponSprite;
        weaponName.text = weapon.weaponData.weaponName;
        weaponCls.text = EnumAttrs.getWeaponCls(weapon.weaponData.weaponCls);
        
        weaponAttrs.ClearClauses();

        weaponAttrs.genClause(weapon.weaponData.damage);
        weaponAttrs.genClause(weapon.weaponData.fireRate);
        weaponAttrs.genClause(weapon.weaponData.range);
        weaponAttrs.genClause(weapon.weaponData.otherEffects);
        
        weaponAttrs.genSpecialInfo(weapon.weaponData.specialInfo);

        buyBtn.GetComponentInChildren<TMP_Text>().text = weapon.weaponData.weaponPrice.ToString();
    }

    public void Buy() {
        weapon.Buy();
        Store.Instance.DeactivateLayout();
        Destroy(gameObject);
        PlayerStatsPanel.Instance.RefreshStatsPanel();
    }
}
