using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanelGrid : MonoBehaviour
{
    [SerializeField] Image weaponBackground;
    [SerializeField] Image weaponImage;
    [SerializeField] WeaponPanelGridInfo weaponPanelGridInfo;

    Button infoBtn;
    StoreWeaponBase weapon;

    private void Awake() {
        infoBtn = GetComponent<Button>();
    }

    public void SetWeaponGrid(int index) {
        weapon = GameManager.Instance.playerWeapons[index];
        weaponImage.sprite = weapon.weaponData.weaponSprite;
        weaponBackground.color = GameManager.Instance.bgColors[weapon.weaponLevel];
        infoBtn.onClick.AddListener(() => weaponPanelGridInfo.SetWeaponInfoPanel(index));
    }

}
