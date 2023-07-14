using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponPanelGridInfo : MonoBehaviour
{
    [Header("Weapon Info")]
    [SerializeField] StoreWeaponBase weapon;
    [SerializeField] Image weaponImage;
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text weaponCls;
    [SerializeField] WeaponAttrs weaponAttrs;

    [Header("Buttons")]
    [SerializeField] Button CraftBtn;
    [SerializeField] Button SaleBtn;
    [SerializeField] Button CancelBtn;

    int index;
    int weaponLevel;

    private void OnEnable() {
        RefeshGrid();
        // WeaponPanel.Instance.SetMaskState(true);
        CancelBtn.onClick.AddListener(OnCancelBtnClicked);
        SaleBtn.onClick.AddListener(OnSaleBtnClicked);
        CraftBtn.onClick.AddListener(OnCraftBtnClicked);
    }

    private void OnDisable() {
        // WeaponPanel.Instance.SetMaskState(false);
        CancelBtn.onClick.RemoveListener(OnCancelBtnClicked);
        SaleBtn.onClick.RemoveListener(OnSaleBtnClicked);
        CraftBtn.onClick.RemoveListener(OnCraftBtnClicked);
    }

    public void SetWeaponInfoPanel(int index) {
        this.weapon = GameManager.Instance.playerWeapons[index];
        this.weaponLevel = weapon.weaponLevel;
        this.index = index;
        if (this.weaponLevel == 4) CraftBtn.gameObject.SetActive(false);
        RefeshGrid();
        WeaponPanel.Instance.SetMaskState(true);
        gameObject.SetActive(true);
    }

    void RefeshGrid() {
        weaponImage.sprite = weapon.weaponData.weaponSprite;
        weaponName.text = weapon.weaponData.weaponName;
        weaponCls.text = EnumAttrs.getWeaponCls(weapon.weaponData.weaponCls);
        
        weaponAttrs.ClearClauses();

        weaponAttrs.genClause(weapon.weaponData.damage, weaponLevel);
        weaponAttrs.genClause(weapon.weaponData.fireRate, weaponLevel);
        weaponAttrs.genClause(weapon.weaponData.range, weaponLevel);
        weaponAttrs.genClause(weapon.weaponData.otherEffects, weaponLevel);
        
        weaponAttrs.genSpecialInfo(weapon.weaponData.specialInfo);
    }

    void OnCancelBtnClicked() {
        gameObject.SetActive(false);
        WeaponPanel.Instance.SetMaskState(false);
    }

    void OnSaleBtnClicked() {
        GameManager.Instance.SaleWeapon(index);
        OnCancelBtnClicked();
    }

    void OnCraftBtnClicked() {
        GameManager.Instance.CraftWeapon(index);
        OnCancelBtnClicked();
    }

}
