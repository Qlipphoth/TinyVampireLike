using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏中所有枪的基类
/// </summary>
public class Gun : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] PlayerInput input;

    [Header("Bullets & Shells")]
    [SerializeField] GameObject bulletPrefab;  // 子弹预制体
    [SerializeField] GameObject shellPrefab;  // 弹壳预制体
    [SerializeField] Transform muzzlePoint;  // 枪口位置
    [SerializeField] Transform shellPoint;  // 弹壳弹出位置

    [Header("Weapon stats")]
    [SerializeField] float fireRate = 1f;  // 射速
    [SerializeField] float damage;  // 设置子弹伤害
    [SerializeField] LayerMask enemyLayer;  // 敌人层

    [Header("Audio")]
    [SerializeField] AudioData fireAudio;  // 射击音效

    Vector2 gunDirection;
    Animator animator;
    WaitForSeconds waitForFireInterval;
    GameObject bulletObject;
    Bullet bullet;
    Collider2D[] colliders;
    CircleCollider2D gunRange;
    float fireTimer;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        gunRange = GetComponent<CircleCollider2D>();
        waitForFireInterval = new WaitForSeconds(fireRate);
    }

    private void Update() {
        AutoFollowenemy();
        fireTimer -= Time.deltaTime;
    }

    private void OnEnable() {
        // input.OnFireEvent += Fire;
        // input.OnStopFireEvent += StopFire;
        // StartCoroutine(nameof(FollowMouseCoroutine));
    }

    private void OnDisable() {
        // input.OnFireEvent -= Fire;
        // input.OnStopFireEvent -= StopFire;
        StopAllCoroutines();
    }

    void Fire() {
        StartCoroutine(nameof(FireCoroutine));
    }

    void StopFire() {
        StopCoroutine(nameof(FireCoroutine));
    }

    void SingleFire() {
        animator.SetTrigger("Fire");
        // AudioManager.Instance.PlayRandomSFX(fireAudio);
        AudioManager.Instance.PoolPlayRandomSFX(fireAudio);
        
        bulletObject = PoolManager.Release(bulletPrefab, muzzlePoint.position, Quaternion.identity);
        bullet = bulletObject.GetComponent<Bullet>();
        bullet.SetSpeed(Quaternion.Euler(0f, 0f, Random.Range(-5f, 5f)) * gunDirection);
        bullet.SetDamage(damage);
        
        PoolManager.Release(shellPrefab, shellPoint.position, shellPoint.rotation);
    }

    IEnumerator FireCoroutine() {
        while (true) {
            SingleFire();
            yield return waitForFireInterval;
        }
    }

    IEnumerator FollowMouseCoroutine() {
        while (true) {
            gunDirection = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
            transform.right = gunDirection;  // 朝向鼠标
            yield return null;
        }
    }

    void AutoFollowenemy() {
        colliders = Physics2D.OverlapCircleAll(transform.position, gunRange.radius, enemyLayer);
        if (colliders.Length == 0) {
            transform.right = Vector2.right;  // 朝向右边
            return;
        }
        foreach (var collider in colliders) {
            if (collider.TryGetComponent<EnemyController>(out EnemyController enemy) && !enemy.IsDead) {
                gunDirection = ((Vector2)(enemy.transform.position - transform.position)).normalized;
                transform.right = gunDirection;  // 朝向敌人
                if (fireTimer <= 0f) {
                    SingleFire();
                    fireTimer = fireRate;
                }
            }
        }
    }

}
