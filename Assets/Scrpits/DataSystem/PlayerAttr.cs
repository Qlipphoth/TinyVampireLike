using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责汇总玩家的各种属性，对外提供改变属性的方法
/// </summary>
public class PlayerAttr : Singleton<PlayerAttr>
{
    [Header("Player Attributes")]
    [SerializeField] int maxHealth = 10;      // 玩家的生命值上限
    [SerializeField] int healthRegeRate = 0;  // 血量回复速度, 单位：% / s, 最终的回复速度为每秒回复：maxHealth * healthRegeRate / 100
    [SerializeField] int damageFactor = 100;  // 伤害倍率，单位：%，最终的伤害值为：damage * damageFactor / 100
    [SerializeField] int attackRangeFactor = 0;  // 攻击范围，单位：%。最终的攻击范围为：weaponAttackRange * (1 + attackRangeFactor / 100)  
    [SerializeField] int armor = 0;           // 护甲值，单位：%。最终的伤害值为：damage * (1 - Armor / 100)
    [SerializeField] int criticalRate = 0;    // 暴击率，单位：%。最终的暴击率为：criticalRate / 100
    [SerializeField] int criticalDamage = 50; // 暴击伤害，单位：%。最终的暴击伤害为：damage * (1 + criticalDamage / 100)
    [SerializeField] int attackSpeed = 0;     // 攻击速度，单位：%，最终武器的攻击间隔为：weaponAttackInterval * (1 - attackSpeed / 100)
    [SerializeField] int dodgeRate = 0;       // 闪避率，单位：%。最终的闪避率为：dodgeRate / 100
    [SerializeField] int moveSpeedFactor = 0; // 移动速度，单位：%。最终的移动速度为：moveSpeed * (1 + moveSpeedFactor / 100)
    [SerializeField] int pickUpRangeFactor = 0; // 捡起物品的范围，单位：%。最终的捡起物品的范围为：pickUpRange * (1 + pickUpRangeFactor / 100)

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int HealthRegeRate { get => healthRegeRate; set => healthRegeRate = value; }
    public int DamageFactor { get => damageFactor; set => damageFactor = value; }
    public int AttackRangeFactor { get => attackRangeFactor; set => attackRangeFactor = value; }
    public int Armor { get => armor; set => armor = value; }
    public int CriticalRate { get => criticalRate; set => criticalRate = value; }
    public int CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
    public int AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public int DodgeRate { get => dodgeRate; set => dodgeRate = value; }
    public int MoveSpeedFactor { get => moveSpeedFactor; set => moveSpeedFactor = value; }
    public int PickUpRangeFactor { get => pickUpRangeFactor; set => pickUpRangeFactor = value; }

    public void ChangeMaxHealth(int value) => maxHealth += value;
    public void ChangeHealthRegeRate(int value) => healthRegeRate += value;
    public void ChangeDamageFactor(int value) => damageFactor += value;
    public void ChangeAttackRangeFactor(int value) => attackRangeFactor += value;
    public void ChangeArmor(int value) => armor += value;
    public void ChangeCriticalRate(int value) => criticalRate += value;
    public void ChangeCriticalDamage(int value) => criticalDamage += value;
    public void ChangeAttackSpeed(int value) => attackSpeed += value;
    public void ChangeDodgeRate(int value) => dodgeRate += value;
    public void ChangeMoveSpeedFactor(int value) => moveSpeedFactor += value;
    public void ChangePickUpRangeFactor(int value) => pickUpRangeFactor += value;

    public delegate void ChangePlayerAttr(int value);
    public static Dictionary<int, ChangePlayerAttr> ChangePlayerAttrDict;

    protected override void Awake() {
        base.Awake();
        ChangePlayerAttrDict = new Dictionary<int, ChangePlayerAttr> {
            {0, ChangeMaxHealth},
            {1, ChangeHealthRegeRate},
            {2, ChangeDamageFactor},
            {3, ChangeAttackRangeFactor},
            {4, ChangeArmor},
            {5, ChangeCriticalRate},
            {6, ChangeCriticalDamage},
            {7, ChangeAttackSpeed},
            {8, ChangeDodgeRate},
            {9, ChangeMoveSpeedFactor},
            {10, ChangePickUpRangeFactor}
        };
    }

    private void OnDisable() {
        ChangePlayerAttrDict.Clear();
    }

}
