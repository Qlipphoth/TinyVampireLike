using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootOnWorld", menuName = "Loots/LootOnWorld")]
public class LootOnWorld : ScriptableObject
{
    [SerializeField] GameObject lootPrefab;
    [SerializeField, Range(0f, 100f)] float dropRate;

    GameObject loot;
    float dropForce = 10f;

    public void LootSpwan(Vector2 position) {
        if (Random.Range(0f, 100f) <= dropRate) {
            loot = PoolManager.Release(lootPrefab, position);
            loot.GetComponent<Rigidbody2D>().AddForce(
                Random.insideUnitCircle * dropForce, ForceMode2D.Impulse);
        }
    }

}
