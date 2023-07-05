using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    [Header("Bullet Settings")]
    [SerializeField] float speed = 10f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float ActivateTime = 3f;
    [SerializeField] float criticalRate = 0.1f;

    [Header("Popup Text")]
    [SerializeField] GameObject popupTextPrefab;

    float damage = 5f;
    new Rigidbody2D rigidbody2D;
    WaitForSeconds waitForActivateTime;
    PopupText_Ani popupText;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        waitForActivateTime = new WaitForSeconds(ActivateTime);
    }

    private void OnEnable() {
        StartCoroutine(nameof(AutoDeactivate));
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        PoolManager.Release(explosionPrefab, transform.position, Quaternion.identity);
        if (other.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy)) {
            
            popupText = PoolManager.Release(popupTextPrefab, enemy.transform.position, 
                    Quaternion.identity).GetComponentInChildren<PopupText_Ani>();
            
            if (Random.Range(0f, 1f) < criticalRate) {
                float curDamage = damage * Random.Range(1.5f, 2f);
                enemy.TakeDamage(curDamage);
                popupText.SetText((int)curDamage, true);
            }
            else {
                enemy.TakeDamage(damage);
                popupText.SetText((int)damage, false);
            }
            // enemy.TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }

    // private void OnColliderEnter2D(Collider2D other) {
    //     PoolManager.Release(explosionPrefab, transform.position, Quaternion.identity);
    //     if (other.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy)) {
    //         enemy.TakeDamage(damage);
    //     }
    //     gameObject.SetActive(false);
    // }

    IEnumerator AutoDeactivate() {
        yield return waitForActivateTime;
        gameObject.SetActive(false);
    }

    public void SetSpeed(Vector2 direction) {
        rigidbody2D.velocity = direction * speed;
    }

    public void  SetDamage(float damage) {
        this.damage = damage;
    }

}
