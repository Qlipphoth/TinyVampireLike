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
    [SerializeField] int healthRegeRate = 0;  // 生命值回复速度，单位：/ 10s, 值为 10 的话，即每 10s 回复 10 点生命值
    [SerializeField] int damageFactor = 0;    // 伤害值，单位：%。最终的伤害值为：damage * (1 + damageFactor / 100)
    [SerializeField] int attackRangeFactor = 0;  // 攻击范围，单位：%。最终的攻击范围为：weaponAttackRange * (1 + attackRangeFactor / 100)  
    [SerializeField] int armor = 0;           // 护甲值，单位：%。最终的伤害值为：damage * (1 - Armor / 100)
    [SerializeField] int criticalRate = 10;    // 暴击率，单位：%。最终的暴击率为：criticalRate / 100
    [SerializeField] int criticalDamage = 50; // 暴击伤害，单位：%。最终的暴击伤害为：damage * (1 + criticalDamage / 100)
    [SerializeField] int attackSpeed = 0;     // 攻击速度，单位：%，最终武器的攻击间隔为：weaponAttackInterval * (1 - attackSpeed / 100)
    [SerializeField] int dodgeRate = 0;       // 闪避率，单位：%。最终的闪避率为：dodgeRate / 100
    [SerializeField] int moveSpeedFactor = 0; // 移动速度，单位：%。最终的移动速度为：moveSpeed * (1 + moveSpeedFactor / 100)
    [SerializeField] int pickUpRangeFactor = 0; // 捡起物品的范围，单位：%。最终的捡起物品的范围为：pickUpRange * (1 + pickUpRangeFactor / 100)

    [Header("Other Attrs")]
    [SerializeField] int gemNum = 100;          // 宝石数量
    [SerializeField] int level = 1;             // 玩家等级
    [SerializeField] float currentEXP = 0;        // 当前经验值
    [SerializeField] float maxEXP = 100;          // 当前等级的最大经验值

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

    public int GemNum { get => gemNum; set => gemNum = value; }
    public int Level { get => level; set => level = value; }
    public float CurrentEXP { get => currentEXP; set => currentEXP = value; }
    public float MaxEXP { get => maxEXP; set => maxEXP = value; }

    public List<int> GetPlayerAttrs() {
        return new List<int> {
            maxHealth,
            healthRegeRate,
            damageFactor,
            attackRangeFactor,
            armor,
            criticalRate,
            criticalDamage,
            attackSpeed,
            dodgeRate,
            moveSpeedFactor,
            pickUpRangeFactor,
        };
    }

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
    
    public void ChangeGemNum(int value) => gemNum += value;
    public void ChangeCurrentEXP(int value) => currentEXP += value;


    public delegate void ChangePlayerAttr(int value);
    public static Dictionary<EnumAttrs.PlayerAttrs, ChangePlayerAttr> ChangePlayerAttrFuncDict;

    protected override void Awake() {
        base.Awake();
        ChangePlayerAttrFuncDict = new Dictionary<EnumAttrs.PlayerAttrs, ChangePlayerAttr> {
            {EnumAttrs.PlayerAttrs.MAXHEALTH, ChangeMaxHealth},
            {EnumAttrs.PlayerAttrs.HEALTHREGERATE, ChangeHealthRegeRate},
            {EnumAttrs.PlayerAttrs.DAMAGEFACTOR, ChangeDamageFactor},
            {EnumAttrs.PlayerAttrs.ATTACKRANGEFACTOR, ChangeAttackRangeFactor},
            {EnumAttrs.PlayerAttrs.ARMOR, ChangeArmor},
            {EnumAttrs.PlayerAttrs.CRITICALRATE, ChangeCriticalRate},
            {EnumAttrs.PlayerAttrs.CRITICALDAMAGE, ChangeCriticalDamage},
            {EnumAttrs.PlayerAttrs.ATTACKSPEED, ChangeAttackSpeed},
            {EnumAttrs.PlayerAttrs.DODGERATE, ChangeDodgeRate},
            {EnumAttrs.PlayerAttrs.MOVESPEEDFACTOR, ChangeMoveSpeedFactor},
            {EnumAttrs.PlayerAttrs.PICKUPRANGEFACTOR, ChangePickUpRangeFactor},
        };
    }

    private void OnEnable() {
        GameEvents.LevelUp += LevelUp;
    }

    private void OnDisable() {
        ChangePlayerAttrFuncDict.Clear();
    }

    public static ChangePlayerAttr GetChangePlayerAttrFunc(EnumAttrs.PlayerAttrs playerAttr) 
        => ChangePlayerAttrFuncDict[playerAttr];

    public void LevelUp() {
        currentEXP -= maxEXP;
        maxEXP *= 1.2f;
        level++;
    }

}
