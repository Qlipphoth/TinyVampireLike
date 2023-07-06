using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    public bool IsDead => enemyIsDead;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 0.2f;
    
    [Header("Raycast")]
    [SerializeField] float raycastDistance = 0.2f;
    [SerializeField] LayerMask raycastLayerMask;
    [SerializeField] bool enemyIsDead = false;

    [Header("Hit")]
    [SerializeField] GameObject hitVFX;
    [SerializeField] AudioData hitAudioData; 
    [SerializeField] float shakeTime;
    [SerializeField] float shakeStrength;

    LootSpawner lootSpawner;
    new CircleCollider2D collider2D;

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    WaitForSeconds waitForDie = new WaitForSeconds(1f);
    WaitUntil waitUntilPathNotBlocked, waitUntilEnemyIsDead;
    
    protected GameObject target;
    Coroutine moveCoroutine;
    Ray ray;
    bool isBlocked;

    void AE_Die() => enemyIsDead = true;

    protected override void Awake() {
        base.Awake();
        collider2D = GetComponent<CircleCollider2D>();
        lootSpawner = GetComponent<LootSpawner>();

        waitUntilPathNotBlocked = new WaitUntil(() => !IsPathBlocked());
        waitUntilEnemyIsDead = new WaitUntil(() => enemyIsDead);
        target = GameObject.FindGameObjectWithTag("Player");
    }


    protected override void OnEnable() {
        base.OnEnable();

        rigidbody2D.drag = 100f;
        enemyIsDead = false;
        // spriteRenderer.color = Color.white;  // 无效

        StartCoroutine(nameof(MoveCoroutine));
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.TryGetComponent(out Player player)) {
            player.TakeDamage(1f);
            AudioManager.Instance.PoolPlayRandomSFX(hitAudioData);
            // 特效的方向直接与运动方向相同就能达到不错的效果，不需要获取碰撞方向
            PoolManager.Release(hitVFX, player.transform.position,
                Quaternion.Euler(0f, 0f, Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg));
        
            AttackSense.Instance.CameraShake(shakeTime, shakeStrength);
        }
    }

#region Die

    public override void Die() {
        enemyIsDead = true;
        rigidbody2D.drag = 5f;
        EnemyManager.Instance.RemoveEnemy(gameObject);

        lootSpawner.SpawnLoots(transform.position);

        StopCoroutine(nameof(MoveCoroutine));
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine() {
        animator.SetTrigger("Die");
        // Debug.Log("Enemy is dying");
        yield return waitForDie;
        // Debug.Log("Enemy is dead");
        gameObject.SetActive(false);
    }

#endregion

#region Move
    
    IEnumerator MoveDirectCoroutine() {
        FlipCharacter();
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);
        yield return waitForFixedUpdate;
    }

    IEnumerator MoveCoroutine() {
        while (gameObject.activeSelf) {
            moveDirection = (target.transform.position - transform.position).normalized;
            yield return waitUntilPathNotBlocked;
            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(nameof(MoveDirectCoroutine));
        }
    }

    private bool IsPathBlocked() {
        ray = new Ray(transform.position, moveDirection);
        collider2D.enabled = false;
        isBlocked = Physics2D.Raycast(ray.origin, ray.direction, raycastDistance, raycastLayerMask);
        collider2D.enabled = true;
        return isBlocked;
    }

#endregion

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(ray.origin, ray.direction * 0.2f);
    // }

}
