using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanelGrid : MonoBehaviour
{
    [SerializeField] Image weaponImage;
    [SerializeField] WeaponPanelGridInfo weaponPanelGridInfo;
    [SerializeField] int index;

    Button infoBtn;

    private void Awake() {
        infoBtn = GetComponent<Button>();
    }

    public void SetWeaponGrid(int index) {
        weaponImage.sprite = GameManager.Instance.
            playerWeapons[index].weaponData.weaponSprite;
        this.index = index;
        infoBtn.onClick.AddListener(() => weaponPanelGridInfo.SetWeaponInfoPanel(index));
    }

}
