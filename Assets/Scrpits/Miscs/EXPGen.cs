using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPGen : MonoBehaviour
{
    [SerializeField] float genRange;
    LootSpawner lootSpawner;

    private void Awake() {
        lootSpawner = GetComponent<LootSpawner>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            lootSpawner.SpawnLoots(
                (Vector2)transform.position + Random.insideUnitCircle * genRange);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, genRange);
    }
}
