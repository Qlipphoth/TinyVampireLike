using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("HUD")]
    [SerializeField] HUDHPBar hudHP;
    [SerializeField] GameObject HpRegenEffect;
    [SerializeField] GameObject dodgeEffect;
    [SerializeField] GameObject popUpTextPrefab;
    [SerializeField] AudioData hpRegenSfx;

    [Header("Weapon Position")]
    [SerializeField] Transform weaponPosition2;
    [SerializeField] Transform weaponPosition4;
    [SerializeField] Transform weaponPosition6;

    [Header("Input")]
    [SerializeField] PlayerInput input;

    [Header("Move")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float accelerationTime = 3f;
    [SerializeField] float decelerationTime = 3f;
    
    [Header("Player Stats")]
    [SerializeField] float pickUpRange = 1f;
    [SerializeField] LayerMask pickUpLayerMask;
    
    [Header("Hurt")]
    [SerializeField] float mutekiTime = 1f;
    [SerializeField] AudioData hurtAudio;

    Collider2D playerCollider;
    PopupText_Ani popupText;

    Vector2 curVelocity;
    float elapsedTime;

    Coroutine moveCoroutine, HpRegenCoroutine;
    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    WaitForSeconds waitForMuteki;
    Collider2D[] colliders; 

    Gun curWeaopn;

    protected override void Awake() {
        base.Awake();
        playerCollider = GetComponent<Collider2D>();
        waitForMuteki = new WaitForSeconds(mutekiTime);
    }

    private void Start() {
        input.EnableGameplayInput();
        hudHP.Initialize(health, maxHealth);
    }

    private void Update() {
        PickUp();
    }

    protected override void OnEnable() {
        base.OnEnable();
        input.OnMoveEvent += Move;
        input.OnStopMoveEvent += StopMove;
        GameEvents.LevelUp += LevelUp;
    }

    private void OnDisable() {
        input.OnMoveEvent -= Move;
        input.OnStopMoveEvent -= StopMove;
        GameEvents.LevelUp -= LevelUp;
    }

#region Override

    public override void TakeDamage(float damage) {
        KeyValuePair<float, bool> damageInfo = DamageManager.Instance.GetEnemyDamage(damage);
        if (!damageInfo.Value) {
            PoolManager.Release(dodgeEffect, transform.position, Quaternion.identity);
            return;
        }

        base.TakeDamage(damageInfo.Key);
        popupText = PoolManager.Release(popUpTextPrefab, transform.position, 
                    Quaternion.identity).GetComponentInChildren<PopupText_Ani>();
        popupText.SetText(damageInfo.Key, false);
        hudHP.UpdateStates(health, maxHealth);


        AudioManager.Instance.PoolPlayRandomSFX(hurtAudio);
        AttackSense.Instance.CameraShake(0.3f, 0.08f);
        
        if (gameObject.activeSelf) {
            StartCoroutine(nameof(MutekiCoroutine));
            if (PlayerAttr.Instance.HealthRegeRate > 0) {
                if (HpRegenCoroutine != null) StopCoroutine(HpRegenCoroutine);
                HpRegenCoroutine = StartCoroutine(HealthRegenCoroutine(10f / PlayerAttr.Instance.HealthRegeRate));
            }
        }
    }

    public override void Die() {
        hudHP.UpdateStates(0, maxHealth);
        base.Die();
        GameEvents.GameOver?.Invoke();
    }

#endregion

#region MuTeKi 

    IEnumerator MutekiCoroutine() {
        playerCollider.enabled = false;
        yield return waitForMuteki;
        playerCollider.enabled = true;
    }

#endregion

#region Move

    private void Move(Vector2 moveInput) {
        moveDirection = moveInput.normalized;
        if (moveCoroutine != null) {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime, moveDirection * moveSpeed));
        FlipCharacter();
    }

    private void StopMove() {
        if (moveCoroutine != null) {
            StopCoroutine(moveCoroutine);
        }
        moveDirection = Vector2.zero;
        moveCoroutine = StartCoroutine(MoveCoroutine(decelerationTime, Vector2.zero));
    }

    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity) {
        elapsedTime = 0f;
        curVelocity = rigidbody2D.velocity;

        while (elapsedTime < time) {
            rigidbody2D.velocity = Vector2.Lerp(curVelocity, moveVelocity, elapsedTime / time);
            elapsedTime += Time.fixedDeltaTime;
            animator.SetFloat(String2Num.SPEED, rigidbody2D.velocity.magnitude);
            yield return waitForFixedUpdate;
        }

        rigidbody2D.velocity = moveVelocity;
    }

#endregion

#region PickUp

    void PickUp() {
        colliders = Physics2D.OverlapCircleAll(transform.position, pickUpRange, pickUpLayerMask);
        if (colliders.Length > 0) {
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.TryGetComponent(out LootItem lootItem)) {
                    lootItem.StartCoroutine(nameof(lootItem.MoveCoroutine));
                }
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }

    #endregion

#region HPRegen

    public void RestoreHealth(float value) {
        if (health == maxHealth) return;
        health = Mathf.Clamp(health + value, 0, maxHealth);

        if (showOnHeadHealthBar) {
            onHeadHealthBar.UpdateStates(health, maxHealth);
        }
        hudHP.UpdateStates(health, maxHealth);
    }

    protected IEnumerator HealthRegenCoroutine(float Interval) {
        WaitForSeconds waitForinterval = new WaitForSeconds(Interval);
        while (health < maxHealth) {
            yield return waitForinterval;
            RestoreHealth(1);
            // AudioManager.Instance.PoolPlayRandomSFX(hpRegenSfx);
            PoolManager.Release(HpRegenEffect, transform.position, Quaternion.identity);
        }
    }

#endregion

#region LevelUp

    public void LevelUp() {
        PlayerAttr.Instance.ChangeMaxHealth(5);
        maxHealth = PlayerAttr.Instance.MaxHealth;
        health = maxHealth;
        hudHP.UpdateStates(health, maxHealth);
        onHeadHealthBar.UpdateStates(health, maxHealth);
    }

#endregion

#region Miscs

    private void DestoryChild(Transform parent) {
        foreach (Transform child in parent) {
            if (child.childCount > 0) Destroy(child.GetChild(0).gameObject);
        }
    }

    public void SetWeaponsPos(List<GameObject> weapons) {
        DestoryChild(weaponPosition2);
        DestoryChild(weaponPosition4);
        DestoryChild(weaponPosition6);

        int weaponNum = weapons.Count;
        Transform WeaponPos = weaponNum > 4 ? weaponPosition6 : 
            weaponNum > 2 ? weaponPosition4 : weaponPosition2;

        for (int i = 0; i < weaponNum; i++) {
            Instantiate(weapons[i], WeaponPos.GetChild(i));
        }
    }

    public void SetWeaponsPos(List<StoreWeaponBase> weapons) {
        DestoryChild(weaponPosition2);
        DestoryChild(weaponPosition4);
        DestoryChild(weaponPosition6);

        int weaponNum = weapons.Count;
        Transform WeaponPos = weaponNum > 4 ? weaponPosition6 : 
            weaponNum > 2 ? weaponPosition4 : weaponPosition2;

        for (int i = 0; i < weaponNum; i++) {
            curWeaopn = Instantiate(weapons[i].weaponPrefab, WeaponPos.GetChild(i)).GetComponent<Gun>();
            curWeaopn.SetGunAttrs(weapons[i].weaponData, weapons[i].weaponLevel);
            curWeaopn.SetGunMaterial(ResourcesManager.Instance.LoadWeaponMaterial(
                weapons[i].weaponData.weaponName, weapons[i].weaponLevel));
        }
    }

#endregion

# region Reset

    private void UpdateAttrs() {
        maxHealth = PlayerAttr.Instance.MaxHealth;
        moveSpeed = 5 * (1 + (float)PlayerAttr.Instance.MoveSpeedFactor / 100);
        pickUpRange = 1 * (1 + (float)PlayerAttr.Instance.PickUpRangeFactor / 100);
    }

    public void ResetPlayer() {
        UpdateAttrs();
        health = maxHealth;
        hudHP.Initialize(health, maxHealth);
        onHeadHealthBar.Initialize(health, maxHealth);

        playerCollider.enabled = true;
        gameObject.SetActive(true);
        transform.position = Vector3.zero;

        input.EnableGameplayInput();
        SetWeaponsPos(GameManager.Instance.playerWeapons);
    }

# endregion

}
