using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumAttrs : MonoBehaviour
{
    public enum ItemCls
    {
        ITEM,
    }

    public enum WeaponCls
    {
        GUN,
    }

    public enum WeaponAttrs {
        DAMAGE,
        FIRERATE,
        RANGE,
        KNOCKBACK,
        PIERCE,
    }

    public enum PlayerAttrs {
        MAXHEALTH,
        HEALTHREGERATE,
        DAMAGEFACTOR,
        ATTACKRANGEFACTOR,
        ARMOR,
        CRITICALRATE,
        CRITICALDAMAGE,
        ATTACKSPEED,
        DODGERATE,
        MOVESPEEDFACTOR,
        PICKUPRANGEFACTOR,
    }

    static Dictionary<ItemCls, string> ItemCls2String;
    static Dictionary<WeaponCls, string> WeaponCls2String;
    static Dictionary<WeaponAttrs, string> WeaponAttrs2String;
    static Dictionary<PlayerAttrs, string> PlayerAttrs2String;
    // static Dictionary<PlayerAttrs, KeyValuePair<string, int>> playerAttrsDict;

    private void Awake() {
        ItemCls2String = new Dictionary<ItemCls, string> {
            {ItemCls.ITEM, "Item"},
        };

        WeaponCls2String = new Dictionary<WeaponCls, string> {
            {WeaponCls.GUN, "Gun"},
        };

        WeaponAttrs2String = new Dictionary<WeaponAttrs, string> {
            {WeaponAttrs.DAMAGE, "Damage"},
            {WeaponAttrs.FIRERATE, "FireRate"},
            {WeaponAttrs.RANGE, "Range"},
            {WeaponAttrs.KNOCKBACK, "KnockBack"},
            {WeaponAttrs.PIERCE, "Pierce"},
        };

        PlayerAttrs2String = new Dictionary<PlayerAttrs, string> {
            {PlayerAttrs.MAXHEALTH, "MaxHealth"},
            {PlayerAttrs.HEALTHREGERATE, "HealthRege"},
            {PlayerAttrs.DAMAGEFACTOR, "Damage"},
            {PlayerAttrs.ATTACKRANGEFACTOR, "AttackRange"},
            {PlayerAttrs.ARMOR, "Armor"},
            {PlayerAttrs.CRITICALRATE, "CriticalRate"},
            {PlayerAttrs.CRITICALDAMAGE, "CriticalDamage"},
            {PlayerAttrs.ATTACKSPEED, "AttackSpeed"},
            {PlayerAttrs.DODGERATE, "DodgeRate"},
            {PlayerAttrs.MOVESPEEDFACTOR, "MoveSpeed"},
            {PlayerAttrs.PICKUPRANGEFACTOR, "PickUpRange"},
        };
    }

    private void OnDisable() {
        ItemCls2String.Clear();
        WeaponCls2String.Clear();
        WeaponAttrs2String.Clear();
        PlayerAttrs2String.Clear();
    }

    public static string getItemCls(ItemCls itemCls) => ItemCls2String[itemCls];
    public static string getWeaponCls(WeaponCls weaponCls) => WeaponCls2String[weaponCls];
    public static string getWeaponAttrKey(WeaponAttrs weaponAttrs) => WeaponAttrs2String[weaponAttrs];
    public static string getPlayerAttrKey(PlayerAttrs playerAttrs) => PlayerAttrs2String[playerAttrs];
    
}
