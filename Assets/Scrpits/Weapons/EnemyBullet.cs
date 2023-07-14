using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] float speed = 5f;
    [SerializeField] float lifeTime = 3f;

    [Header("VFXs")]
    [SerializeField] GameObject hitPrefab;

    [Header("Popup Text")]
    [SerializeField] GameObject popupTextPrefab;

    float damage;
    new Rigidbody2D rigidbody2D;
    PopupText_Ani popupText;

    public void SetSpeed(Vector2 direction) {
        rigidbody2D.velocity = direction * speed;
    }

    public void  SetDamage(float damage) {
        this.damage = damage;
    }

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        StartCoroutine(LifeTime());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<Player>(out Player player)) {
                
            PoolManager.Release(hitPrefab, player.transform.position, Quaternion.Euler(0f, 0f, 
                Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg));
            // TODO: 更简单的角度计算方法

            player.TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }

    IEnumerator LifeTime() {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

}
