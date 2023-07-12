using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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

    [Header("Player Guns")]
    public List<StoreWeaponBase> playerWeapons = new List<StoreWeaponBase>();

    private void Start() {
        OnWaveStart();
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
        PlayerAttr.Instance.LevelUp();
    }

    public void OnWaveEnd() {
        waveManager.gameObject.SetActive(false);
        enemyManager.gameObject.SetActive(false);
        store.gameObject.SetActive(true);
    }

    public void OnWaveStart() {
        player.ResetPlayer();

        InGameGemBar.Initialize();
        hudEXPBar.Initialize(PlayerAttr.Instance.CurrentEXP, PlayerAttr.Instance.MaxEXP);

        store.gameObject.SetActive(false);
        enemyManager.gameObject.SetActive(true);
        waveManager.gameObject.SetActive(true);
    }

}
