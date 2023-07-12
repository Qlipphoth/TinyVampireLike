using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("HUD")]
    [SerializeField] HUDHPBar hudHP;

    [Header("Weapon Position")]
    [SerializeField] Transform weaponPosition2;
    [SerializeField] Transform weaponPosition4;
    [SerializeField] Transform weaponPosition6;

    [Header("Input")]
    [SerializeField] PlayerInput input;

    [Header("Move")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float accelerationTime = 3f;
    [SerializeField] float decelerationTime = 3f;
    
    [Header("Player Stats")]
    [SerializeField] float pickUpRange = 2.5f;
    [SerializeField] LayerMask pickUpLayerMask;
    
    [Header("Hurt")]
    [SerializeField] float mutekiTime = 1f;

    Collider2D playerCollider;

    Vector2 curVelocity;
    float elapsedTime;

    Coroutine moveCoroutine;
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
        TakeDamage(10f);
    }

    private void Update() {
        PickUp();
    }

    protected override void OnEnable() {
        base.OnEnable();
        input.OnMoveEvent += Move;
        input.OnStopMoveEvent += StopMove;
    }

    private void OnDisable() {
        input.OnMoveEvent -= Move;
        input.OnStopMoveEvent -= StopMove;
    }

#region Override

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
        hudHP.UpdateStates(health, maxHealth);

        if (gameObject.activeSelf) {
            StartCoroutine(nameof(MutekiCoroutine));
        }

    }

    public override void Die() {
        hudHP.UpdateStates(0, maxHealth);
        base.Die();
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
        }
    }

#endregion

# region Reset

    public void ResetPlayer() {
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
