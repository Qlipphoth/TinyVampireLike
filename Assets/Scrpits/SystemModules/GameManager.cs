using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("WaveManager")]
    [SerializeField] WaveManager waveManager;

    [Header("EnemyManager")]
    [SerializeField] EnemyManager enemyManager;

    [Header("Store")]
    [SerializeField] Store store;

    [Header("Gem Bars")]
    [SerializeField] GemBar InGameGemBar;
    [SerializeField] GemBar StoreGemBar;

    public void OnGemChangedInGame(int num) {
        PlayerAttr.Instance.ChangeGemNum(num);
        InGameGemBar.StartCoroutine(InGameGemBar.UpdateGemNumCoroutine());
    }
    
    public void OnGemChangedInStore(int num) {
        PlayerAttr.Instance.ChangeGemNum(num);
        StoreGemBar.StartCoroutine(StoreGemBar.UpdateGemNumCoroutine());
    }

    public void OnWaveEnd() {
        waveManager.gameObject.SetActive(false);
        enemyManager.gameObject.SetActive(false);
        store.gameObject.SetActive(true);
    }

    public void GoForNextWave() {
        player.transform.position = Vector3.zero;
        playerInput.EnableGameplayInput();
        store.gameObject.SetActive(false);
        enemyManager.gameObject.SetActive(true);
        waveManager.gameObject.SetActive(true);
    }

}
