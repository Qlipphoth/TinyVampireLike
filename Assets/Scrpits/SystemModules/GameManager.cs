using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Music")]
    [SerializeField] AudioData bgm;

    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Player")]
    [SerializeField] Player player;

    [Header("WaveManager")]
    [SerializeField] WaveManager waveManager;

    [Header("EnemyManager")]
    [SerializeField] EnemyManager enemyManager;

    [Header("Store")]
    [SerializeField] Store store;

    [Header("Gem Bars")]
    [SerializeField] GemBar InGameGemBar;

    [Header("Player EXP")]
    [SerializeField] HUDEXPBar hudEXPBar;
    [SerializeField] AudioData[] levelUpSFX;
    [SerializeField] GameObject  levelUpVFX;

    [Header("Colors")]
    public List<Color> bgColors;

    [Header("Player Start Weapons")]
    public List<StoreWeaponBase> playerStartWeapons = new List<StoreWeaponBase>();

    [Header("Player Objects")]
    public List<StoreWeaponBase> playerWeapons = new List<StoreWeaponBase>();
    public Dictionary<StoreItemBase, int> playerItems = new Dictionary<StoreItemBase, int>();

    StoreWeaponBase curWeapon;

    private void Start() {
        for (int i = 0; i < playerStartWeapons.Count; i++) {
            playerWeapons.Add(Instantiate(playerStartWeapons[i], transform));
        }
        OnWaveStart();
        AudioManager.Instance.PlayMusic(bgm);
    }

    public void OnGemChangedInGame(int num) {
        PlayerAttr.Instance.ChangeGemNum(num);
        InGameGemBar.StartCoroutine(InGameGemBar.UpdateGemNumCoroutine());
    }

    public void ChangeEXP(int exp) {
        PlayerAttr.Instance.ChangeCurrentEXP(exp);
        hudEXPBar.UpdateStates(PlayerAttr.Instance.CurrentEXP, PlayerAttr.Instance.MaxEXP);
    }

    public void LevelUp() {
        AudioManager.Instance.PoolPlayRandomSFX(levelUpSFX);
        PoolManager.Release(levelUpVFX, player.transform.position);
        GameEvents.LevelUp?.Invoke();
    }

    public void SaleWeapon(int index) {
        PlayerAttr.Instance.ChangeGemNum((int)(
            playerWeapons[index].weaponData.weaponPrice * 0.7f));
        DestoryWeapon(index);
        Store.Instance.RefreshGem();
    }

    public void CraftWeapon(int index) {
        for (int i = 0; i < playerWeapons.Count; i++) {
            if ((playerWeapons[i].weaponData.weaponName == playerWeapons[index].weaponData.weaponName) && 
                (playerWeapons[i].weaponLevel == playerWeapons[index].weaponLevel) && (i != index)) {
                playerWeapons[index].weaponLevel++;
                DestoryWeapon(i);
                return;
            }
        }
    }

    public bool CraftWeapon(StoreWeaponBase weapon) {
        for (int i = 0; i < playerWeapons.Count; i++) {
            if ((playerWeapons[i].weaponData.weaponName == weapon.weaponData.weaponName) && 
                (playerWeapons[i].weaponLevel == weapon.weaponLevel)) {
                playerWeapons[i].weaponLevel++;
                return true;
            }
        }
        return false;
    }

    private void DestoryWeapon(int index) {
        curWeapon = playerWeapons[index];
        playerWeapons.RemoveAt(index);
        Destroy(curWeapon.gameObject);
        WeaponPanel.Instance.RefreshWeaponPanel();
    }

    public void OnWaveEnd() {
        if (waveManager.WaveNum == 5) {
            GameEvents.GameWin?.Invoke();
            return;
        }
        waveManager.gameObject.SetActive(false);
        enemyManager.gameObject.SetActive(false);
        store.gameObject.SetActive(true);
    }

    public void OnWaveStart() {
        CameraFollow.Instance.ResetCamera();
        player.ResetPlayer();

        InGameGemBar.Initialize();
        hudEXPBar.Initialize(PlayerAttr.Instance.CurrentEXP, PlayerAttr.Instance.MaxEXP);

        store.gameObject.SetActive(false);
        enemyManager.gameObject.SetActive(true);
        waveManager.gameObject.SetActive(true);
    }

}
