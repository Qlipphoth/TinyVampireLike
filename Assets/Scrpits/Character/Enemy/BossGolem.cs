using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGolem : Enemy
{
    [Header("Player Detection")]
    [SerializeField] Transform playerDetectionPoint;
    [SerializeField] Vector3 playerDetectionSize;
    [SerializeField] LayerMask playerLayer;

    [Header("Move Range")]
    [SerializeField] float changeTargetInterval = 1f;

    [Header("Skill1")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bulletNum = 20;
    [SerializeField] float skill1Interval = 5f;
    [SerializeField] float radius = 5f;

    [Header("SkillLaser")]
    [SerializeField] float skillLaserInterval = 10f;

    Vector2 curTargetPos;
    float skillLaserTimer = 10f;
    float skill1Timer = 5f;
    float changeTargetTimer;
    bool isInSkill, isInRage;

    private void Update() {

        skill1Timer -= Time.deltaTime;
        skillLaserTimer -= Time.deltaTime;
        changeTargetTimer -= Time.deltaTime;

        if (skill1Timer <= 0f && !isInSkill) StartCoroutine(Skill1());
        if (skillLaserTimer <= 0f && !isInSkill && Physics2D.OverlapBox(
            playerDetectionPoint.position, playerDetectionSize, 0f, playerLayer)) {
                StartCoroutine(SkillLaser());
        } 
    }

    protected override void FixedUpdate() {
        Move();
    }

    public override void FlipCharacter() {
        transform.localScale = new Vector3(moveDirection.x > 0 ? 1 : -1, 1, 1);
        onHeadHealthBar.transform.localScale = new Vector3(moveDirection.x > 0 ? 1 : -1, 1, 1);
    }

    private IEnumerator SkillLaser() {
        isInSkill = true;
        animator.SetTrigger(String2Num.LASER);
        skillLaserTimer = skillLaserInterval;
        yield return new WaitForSeconds(5f);
        isInSkill = false;
    }

    private IEnumerator Skill1() {
        isInSkill = true;
        animator.SetTrigger(String2Num.SKILL1);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 25; i++) {
            EnemyBullet bullet = PoolManager.Release(bulletPrefab, transform.position + Random.insideUnitSphere * radius, 
                Quaternion.identity).GetComponent<EnemyBullet>();
            bullet.SetDamage(damage);
        }
        skill1Timer = skill1Interval;
        isInSkill = false;
    }

    private void Move() {
        if (enemyIsDead || IsPathBlocked() || isInSkill) return;
        if (health > maxHealth / 2f) SimpleMove();
        else {
            if (!isInRage) RageMode();
            RageMove();
        }
        FlipCharacter();
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    protected override void SimpleMove() {
        if (changeTargetTimer < 0) {
            moveDirection = (target.transform.position - transform.position).normalized;
            changeTargetTimer = changeTargetInterval;
        }
    }   

    private void RageMove() {
        moveDirection = (target.transform.position - transform.position).normalized;
        FlipCharacter();
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerDetectionPoint.position, playerDetectionSize);
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void RageMode() {
        bulletNum += 10;
        radius *= 1.5f;
        damage *= 1.5f;
        skill1Interval /= 2f;
        skillLaserInterval *= 10f;
        changeTargetInterval /= 2f;
        isInRage = true;
    }

}
