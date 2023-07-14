using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{

    [Header("Bullet Settings")]
    [SerializeField] float speed = 10f;

    [Header("VFXs")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject hitPrefab;


    [Header("Popup Text")]
    [SerializeField] GameObject popupTextPrefab;

    float damage;
    new Rigidbody2D rigidbody2D;
    PopupText_Ani popupText;
    Vector2 hitPoint;
    KeyValuePair<float, bool> damageInfo;

    public void SetSpeed(Vector2 direction) {
        rigidbody2D.velocity = direction * speed;
    }

    public void  SetDamage(float damage) {
        this.damage = damage;
    }

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        hitPoint = other.GetContact(0).point;
        PoolManager.Release(explosionPrefab, hitPoint, Quaternion.identity);
         
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) {
            
            popupText = PoolManager.Release(popupTextPrefab, enemy.transform.position, 
                    Quaternion.identity).GetComponentInChildren<PopupText_Ani>();
            // 特效的方向直接与运动方向相同就能达到不错的效果，不需要获取碰撞方向
            PoolManager.Release(hitPrefab, enemy.transform.position, Quaternion.Euler(0f, 0f, 
                Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg));

            damageInfo = DamageManager.Instance.GetPlayerDamage(damage);
            popupText.SetText(damageInfo.Key, damageInfo.Value);
            enemy.TakeDamage(damageInfo.Key);
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

}
