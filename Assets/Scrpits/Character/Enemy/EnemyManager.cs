using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Enemy Spwan Settings")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject spwanWarning;
    [SerializeField] AudioData[] spawnAudioData;
    [SerializeField] float spawnTime = 3f;
    [SerializeField] float spwanRadius = 1.5f;

    [Header("Boss")]
    [SerializeField] GameObject bossPrefab;
    [SerializeField] Transform bossSpawnPoint;

    List<Vector2> spawnPosList = new List<Vector2>();
    List<GameObject> enemyList = new List<GameObject>();

    List<int> wave2EnemyNumMin = new List<int> {1, 3, 5, 6, 6};
    List<int> wave2EnemyNumMax = new List<int> {5, 6, 10, 10, 10};

    int i;
    Vector2 spawnPos;
    GameObject enemy;
    WaitForSeconds waitSpawnTime;
    WaitForSeconds waitSpawnWarningTime = new WaitForSeconds(1f);
    WaitForSeconds waitSpwanInterval = new WaitForSeconds(0.04f);
    Coroutine spawnBossCoroutine;

    public List<GameObject> allEnemies = new List<GameObject>();
    Enemy curEnemy;

    protected override void Awake() {
        base.Awake();
        waitSpawnTime = new WaitForSeconds(spawnTime);
    }

    private void OnEnable() {
        StartCoroutine(SpawnEnemy());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private int Wave2EnemyNum(int waveNum) {
        return Random.Range(wave2EnemyNumMin[waveNum - 1], wave2EnemyNumMax[waveNum - 1]);
    }

    private List<float> Wave2EnemyAttrs(int waveNum) {
        return new List<float> {
            Random.Range(4, 6f) * waveNum / 2,
            Random.Range(1, 3f) + waveNum,
            Random.Range(1, 2f) + (float)(waveNum / 5), 
        };
    }

    IEnumerator SpawnEnemy() {
        yield return waitSpawnWarningTime;
        while (gameObject.activeSelf) {
            if (WaveManager.Instance.WaveNum == 5 && spawnBossCoroutine == null) {
                spawnBossCoroutine = StartCoroutine(SpawnBoss());
            }
            StartCoroutine(SpawnEnemies(Wave2EnemyNum(WaveManager.Instance.WaveNum)));
            yield return waitSpawnTime;
        }
    }

    IEnumerator SpawnEnemies(int enemyNum) {
        spawnPosList.Clear();
        enemyList.Clear();
        for (i = 0; i < enemyNum; i++) {
            spawnPosList.Add(spawnPoints[Random.Range(0, spawnPoints.Length)].position + Random.insideUnitSphere * spwanRadius);
            enemyList.Add(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
        }

        for (i = 0; i < enemyNum; i++) {
            yield return waitSpwanInterval;
            PoolManager.Release(spwanWarning, spawnPosList[i], Quaternion.identity);
            // AudioManager.Instance.PlayRandomSFX(spawnAudioData);
            AudioManager.Instance.PoolPlayRandomSFX(spawnAudioData);
            
        }
        
        yield return waitSpawnWarningTime;
        
        for (i = 0; i < enemyNum; i++) {
            // yield return waitSpwanInterval;
            enemy = PoolManager.Release(enemyList[i], spawnPosList[i], Quaternion.identity);
            enemy.GetComponent<Enemy>().SetAttrs(Wave2EnemyAttrs(enemyNum));
            allEnemies.Add(enemy);
        }
    }

    IEnumerator SpawnBoss() {
        yield return waitSpawnWarningTime;
        PoolManager.Release(spwanWarning, bossSpawnPoint.position, Quaternion.identity, new Vector3(2f, 2f, 1f));
        yield return waitSpawnWarningTime;
        enemy = PoolManager.Release(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        allEnemies.Add(enemy);
        // TODO: 显示及其他
    }

    public void RemoveEnemy(GameObject enemy) {
        allEnemies.Remove(enemy);
    }

    public void SlayAll(bool isDropLoot = false) {
        // 在 foreach 循环中对调用 Die 对集合进行了修改，会报错
        // foreach (var enemy in allEnemies) {
        //     enemy.GetComponent<EnemyController>().Die();
        // }
        
        // 正向遍历会导致删除元素后索引错位，不能 SlayAll
        // for (int i = 0; i < allEnemies.Count; i++) {
        //     allEnemies[i].GetComponent<EnemyController>().Die();
        // }

        // 只有反向遍历可以 SlayAll
        for (int i = allEnemies.Count - 1; i >= 0; i--) {
            curEnemy = allEnemies[i].GetComponent<Enemy>();
            if (isDropLoot) curEnemy.Die();
            else curEnemy.DieWithoutLoot();
        }
        StopAllCoroutines();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        foreach (var spawnPoint in spawnPoints) {
            Gizmos.DrawWireSphere(spawnPoint.position, spwanRadius);
        }
    }
}
