using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster4 : Enemy
{
    [SerializeField] float fireInterval = 3f;
    [SerializeField] float minDistance = 3f;
    [SerializeField] GameObject bulletPrefab;

    Vector3 targetDirecton;
    float distance;
    float fireTimer;

    WaitForSeconds waitForFireAnim = new WaitForSeconds(0.5f);

    private void OnDisable() {
        StopAllCoroutines();
    }

    protected void Update() {
        Fire();
        fireTimer -= Time.deltaTime;
    }

    protected override void FixedUpdate() {
        SimpleMove();
    }

    protected override void SimpleMove() {
        if (enemyIsDead || IsPathBlocked()) return;
        targetDirecton = target.transform.position - transform.position;
        moveDirection = targetDirecton.normalized;
        FlipCharacter();
        distance = targetDirecton.magnitude;
        if (Mathf.Abs(distance - minDistance) < 0.2f) return;
        else {
            if (distance > minDistance) {
                transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);
            }
            else {
                moveDirection = -moveDirection;
                FlipCharacter();
                transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void Fire() {
        if (fireTimer > 0f || enemyIsDead) return;
        StartCoroutine(nameof(FireCoroutine));
    }

    IEnumerator FireCoroutine() {
        fireTimer = fireInterval;
        animator.SetTrigger(String2Num.ATTACK);
        yield return waitForFireAnim;
        EnemyBullet bullet = PoolManager.Release(bulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bullet.SetSpeed(targetDirecton.normalized);
        bullet.SetDamage(damage);
    }

}
