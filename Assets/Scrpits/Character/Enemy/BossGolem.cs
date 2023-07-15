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
    [SerializeField] AudioData skill1SFX;
    [SerializeField] int bulletNum = 20;
    [SerializeField] float skill1Interval = 5f;
    [SerializeField] float radius = 5f;

    [Header("SkillLaser")]
    [SerializeField] float skillLaserInterval = 10f;
    [SerializeField] AudioData ChargeSFX;
    [SerializeField] AudioData LaserSFX;

    [Header("Rage")]
    [SerializeField] AudioData rageSFX;

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

    public override void Die() {
        base.Die();
        GameEvents.GameWin?.Invoke();
    }

    public override void FlipCharacter() {
        transform.localScale = new Vector3(moveDirection.x > 0 ? 1 : -1, 1, 1);
        onHeadHealthBar.transform.localScale = new Vector3(moveDirection.x > 0 ? 1 : -1, 1, 1);
    }

    private IEnumerator SkillLaser() {
        isInSkill = true;
        AttackSense.Instance.CameraShake(0.5f, 0.05f);
        animator.SetTrigger(String2Num.LASER);
        skillLaserTimer = skillLaserInterval;

        AudioManager.Instance.PoolPlayRandomSFX(ChargeSFX);
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PoolPlayRandomSFX(LaserSFX);
        yield return new WaitForSeconds(3f);
        isInSkill = false;
    }

    private IEnumerator Skill1() {
        isInSkill = true;
        animator.SetTrigger(String2Num.SKILL1);
        AudioManager.Instance.PoolPlayRandomSFX(skill1SFX);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < bulletNum; i++) {
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
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerDetectionPoint.position, playerDetectionSize);
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void RageMode() {
        AudioManager.Instance.PoolPlayRandomSFX(rageSFX);
        bulletNum *= 3;
        radius *= 1.5f;
        damage *= 1.5f;
        moveSpeed += 0.5f;
        skill1Interval /= 2f;
        skillLaserInterval *= 100f;
        skillLaserTimer = skillLaserInterval;
        changeTargetInterval /= 2f;
        isInRage = true;
    }

}
