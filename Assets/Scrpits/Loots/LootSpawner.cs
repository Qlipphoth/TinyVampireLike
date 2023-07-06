using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] LootOnWorld[] loots;
    [SerializeField] float genRange;

    public void SpawnLoots(Vector2 position) {
        foreach (var loot in loots) {
            loot.LootSpwan(position + Random.insideUnitCircle * genRange);
        }
    }

    // private void OnDrawGizmosSelected() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, genRange);
    // }
}
