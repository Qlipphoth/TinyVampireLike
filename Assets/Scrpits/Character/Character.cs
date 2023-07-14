using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary>
/// 角色基类，包含游戏中角色的基本属性和方法，比如血量、死亡等
/// </summary>
public class Character : MonoBehaviour
{
    // [SerializeField] GameObject deathVFX;  // 死亡特效
    
    [Header("Health")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected StatesBar onHeadHealthBar;
    [SerializeField] protected bool showOnHeadHealthBar = true;
    // [SerializeField] AudioData[] deathAudioData;

    new protected Rigidbody2D rigidbody2D;  // 角色刚体
    protected Animator animator;  // 角色动画控制器
    protected SpriteRenderer spriteRenderer;  // 角色精灵渲染器

    protected float health;  // 角色血量
    protected Vector2 moveDirection;  // 角色移动方向

    protected virtual void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        // 角色的模型和动画控制器都在子物体中
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        rigidbody2D.gravityScale = 0f;
    }

    /// <summary>
    /// 初始化血量及血条显示
    /// </summary>
    protected virtual void OnEnable() {
        health = maxHealth;
        if (showOnHeadHealthBar) ShowOnHeadHealthBar();
        else HideOnHeadHealthBar();
    }

    public virtual void FlipCharacter() {
        spriteRenderer.flipX = moveDirection.x < 0 ? true : false;
    }

    /// <summary>
    /// 在角色头顶显示血条
    /// </summary>
    public void ShowOnHeadHealthBar() {
        onHeadHealthBar.gameObject.SetActive(true);
        onHeadHealthBar.Initialize(health, maxHealth);
    }

    /// <summary>
    /// 隐藏角色头顶的血条
    /// </summary>
    public void HideOnHeadHealthBar() {
        onHeadHealthBar.gameObject.SetActive(false);
    }

    public virtual void TakeDamage(float damage) {
        if (health == 0) return;  // 先判断这个会消除下面的 bug
        health -= damage;
        // 不加 && gameObject.activeSelf 可能会在角色死亡后依然调用血条携程，触发bug
        if (showOnHeadHealthBar) {
            onHeadHealthBar.UpdateStates(health, maxHealth);
        }
        if (health <= 0f) {
            // Die();
            health = 0f;
            Die();
        }
    }

    public virtual void Die() {
        // AudioManager.Instance.PlayRandomSFX(deathAudioData);
        // PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }

}
