using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Player")]
    [SerializeField] GameObject playerPos;

    [Header("WaveManager")]
    [SerializeField] WaveManager waveManager;

    [Header("EnemyManager")]
    [SerializeField] EnemyManager enemyManager;

    [Header("Store")]
    [SerializeField] Store store;

    [Header("Gem Bars")]
    [SerializeField] GemBar InGameGemBar;
    [SerializeField] GemBar StoreGemBar;

    [Header("Player Guns")]
    public List<GameObject> playerGuns = new List<GameObject>();
    Player player;

    private void Start() {
        player = playerPos.GetComponent<Player>();
        player.SetWeaponsPos(playerGuns);
    }

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
        playerInput.EnableGameplayInput();
        playerPos.transform.position = Vector3.zero;
        player.SetWeaponsPos(playerGuns);
        
        store.gameObject.SetActive(false);
        enemyManager.gameObject.SetActive(true);
        waveManager.gameObject.SetActive(true);
    }

}
