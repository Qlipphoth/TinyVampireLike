using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] Pool[] enemyPools;
    [SerializeField] Pool[] playerBulletPools;
    // [SerializeField] Pool[] enemyProjectilePools;
    [SerializeField] Pool[] vFXPools;
    [SerializeField] Pool[] textPools;
    [SerializeField] Pool[] lootsPools;
    static Dictionary<GameObject, Pool> prefab2Pool;  // prefab 到对应 pool 的映射

    public void DeActivateAllLoots() {
        foreach (var pool in lootsPools) {
            pool.DeActivateAll();
        }
    }

    protected override void Awake() {
        base.Awake();
        prefab2Pool = new Dictionary<GameObject, Pool>();
        Initialize(enemyPools);
        Initialize(playerBulletPools);
        // Initialize(enemyProjectilePools);
        Initialize(textPools);
        Initialize(vFXPools);
        Initialize(lootsPools);
    }

    #if UNITY_EDITOR
        // 从编辑器停止运行时调用，适合用于 debug
        private void OnDestroy() {
            checkPoolSize(enemyPools);
            checkPoolSize(playerBulletPools);
            checkPoolSize(textPools);
            // checkPoolSize(enemyProjectilePools);
            checkPoolSize(vFXPools);
            checkPoolSize(lootsPools);
        }

    #endif

    void checkPoolSize(Pool[] pools) {
        foreach (var pool in pools) {
            if (pool.RuntimeSize > pool.Size) {
                Debug.LogWarning(
                    $"Pool: {pool.Prefab.name} has a runtime size {pool.RuntimeSize} bigger " +
                    $"than its initial size {pool.Size}!"
                );
            }
        }
    }

    void Initialize(Pool[] pools) {
        foreach (var pool in pools) {
            // 条件编译，当在unity中才启用，保证在其他平台的运行效率
            #if UNITY_EDITOR
                if (prefab2Pool.ContainsKey(pool.Prefab)) {
                    Debug.LogError("Same prefab in multiple Pools! Prefab: " + pool.Prefab.name);
                    continue;  // 防止重复的键
                }
            #endif
            prefab2Pool.Add(pool.Prefab, pool);
            Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }

    /// <summary>
    /// 根据传入参数返回一个对象池中准备好的对象
    /// </summary>
    /// <param name="prefab">指定的对象预制体</param>
    /// <returns>对象池中预备好的对象</returns>
    public static GameObject Release(GameObject prefab) {
        #if UNITY_EDITOR
            if (!prefab2Pool.ContainsKey(prefab)) {
                Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);
                return null;
            }
        #endif
        return prefab2Pool[prefab].preparedObject();
    }

    public static GameObject Release(GameObject prefab, Vector3 position) {
        #if UNITY_EDITOR
            if (!prefab2Pool.ContainsKey(prefab)) {
                Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);
                return null;
            }
        #endif
        return prefab2Pool[prefab].preparedObject(position);
    }

    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation) {
        #if UNITY_EDITOR
            if (!prefab2Pool.ContainsKey(prefab)) {
                Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);
                return null;
            }
        #endif
        return prefab2Pool[prefab].preparedObject(position, rotation);
    }

    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale) {
        #if UNITY_EDITOR
            if (!prefab2Pool.ContainsKey(prefab)) {
                Debug.LogError("Pool Manager could NOT find prefab: " + prefab.name);
                return null;
            }
        #endif
        return prefab2Pool[prefab].preparedObject(position, rotation, localScale);
    }

}
