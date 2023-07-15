using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏中所有枪的基类
/// </summary>
public class Gun : MonoBehaviour
{

    [Header("Bullets & Shells")]
    [SerializeField] GameObject bulletPrefab;  // 子弹预制体
    [SerializeField] GameObject shellPrefab;  // 弹壳预制体
    [SerializeField] Transform muzzlePoint;  // 枪口位置
    [SerializeField] Transform shellPoint;  // 弹壳弹出位置

    [Header("Weapon stats")]
    [SerializeField] LayerMask enemyLayer;  // 敌人层

    [Header("Audio")]
    [SerializeField] AudioData fireAudio;  // 射击音效

    Vector2 gunDirection;
    SpriteRenderer spriteRenderer;
    Animator animator;
    WaitForSeconds waitForFireInterval;
    GameObject bulletObject;
    Bullet bullet;
    Collider2D[] colliders;

    float damage;
    float gunRange;
    float fireRate;
    float fireTimer;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() {
        AutoFollowenemy();
        AutoFlip();
        fireTimer -= Time.deltaTime;
    }

    void SingleFire() {
        animator.SetTrigger("Fire");
        // AudioManager.Instance.PlayRandomSFX(fireAudio);
        // AudioManager.Instance.PoolPlayRandomSFX(fireAudio);
        StartCoroutine(AudioManager.Instance.PoolPlayRandomSFX(fireAudio, Random.Range(0.04f, 0.1f)));
        
        bulletObject = PoolManager.Release(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        bullet = bulletObject.GetComponent<Bullet>();
        bullet.SetSpeed(Quaternion.Euler(0f, 0f, Random.Range(-5f, 5f)) * gunDirection);
        bullet.SetDamage(damage);
        
        PoolManager.Release(shellPrefab, shellPoint.position, shellPoint.rotation);
    }

    void AutoFollowenemy() {
        colliders = Physics2D.OverlapCircleAll(transform.position, 
            DamageManager.Instance.GetFireRange(gunRange), enemyLayer);  // TODO: 优化，不要每帧都获取范围等参数
        if (colliders.Length == 0) {
            gunDirection = Vector2.right;
            transform.right = gunDirection;  // 朝向右边
            return;
        }
        
        Collider2D randomEnemy = colliders[Random.Range(0, colliders.Length)];
        if (randomEnemy.TryGetComponent<Enemy>(out Enemy enemy) && !enemy.IsDead) {
            gunDirection = ((Vector2)(enemy.transform.position - transform.position)).normalized;
            transform.right = gunDirection;  // 朝向敌人
            if (fireTimer <= 0f) {
                SingleFire();
                fireTimer = DamageManager.Instance.GetFireRate(fireRate);
            }
        }
        
        // foreach (var collider in colliders) {
        //     if (collider.TryGetComponent<Enemy>(out Enemy enemy) && !enemy.IsDead) {
        //         gunDirection = ((Vector2)(enemy.transform.position - transform.position)).normalized;
        //         transform.right = gunDirection;  // 朝向敌人
        //         if (fireTimer <= 0f) {
        //             SingleFire();
        //             fireTimer = DamageManager.Instance.GetFireRate(fireRate);
        //         }
        //     }
        
    }

    void AutoFlip() {
        spriteRenderer.flipY = gunDirection.x < 0f;
    }

    public void SetGunAttrs(WeaponData weaponData, int weaponLevel) {
        this.damage = weaponData.damage.value[weaponLevel];
        this.gunRange = weaponData.range.value[weaponLevel];
        this.fireRate = weaponData.fireRate.value[weaponLevel];
    }

    public void SetGunMaterial(Material material) {
        spriteRenderer.material = material;
    }

}
