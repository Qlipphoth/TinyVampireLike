using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponPanelGridInfo : Singleton<WeaponPanelGridInfo>
{
    [Header("Pos")]
    [SerializeField] Vector3[] InfoPanelPos;

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

    private void OnEnable() {
        RefeshGrid();
        // WeaponPanel.Instance.SetMaskState(true);
        CancelBtn.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnDisable() {
        // WeaponPanel.Instance.SetMaskState(false);
        CancelBtn.onClick.RemoveListener(() => gameObject.SetActive(false));
    }

    public void SetWeaponInfoPanel(int index) {
        this.weapon = GameManager.Instance.playerWeapons[index];
        RefeshGrid();
        transform.position = InfoPanelPos[index];
        // gameObject.SetActive(true);
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
    }


}
