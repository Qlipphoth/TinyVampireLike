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

    static public Dictionary<ItemCls, string> ItemCls2String;
    static public Dictionary<WeaponCls, string> WeaponCls2String;
    static public Dictionary<PlayerAttrs, string> PlayerAttrs2String;

    private void Awake() {
        ItemCls2String = new Dictionary<ItemCls, string> {
            {ItemCls.ITEM, "Item"},
        };

        WeaponCls2String = new Dictionary<WeaponCls, string> {
            {WeaponCls.GUN, "Gun"},
        };

        PlayerAttrs2String = new Dictionary<PlayerAttrs, string> {
            {PlayerAttrs.MAXHEALTH, "Max Health"},
            {PlayerAttrs.HEALTHREGERATE, "Health Regeneration"},
            {PlayerAttrs.DAMAGEFACTOR, "Damage Factor"},
            {PlayerAttrs.ATTACKRANGEFACTOR, "Attack Range Factor"},
            {PlayerAttrs.ARMOR, "Armor"},
            {PlayerAttrs.CRITICALRATE, "Critical Rate"},
            {PlayerAttrs.CRITICALDAMAGE, "Critical Damage"},
            {PlayerAttrs.ATTACKSPEED, "Attack Speed"},
            {PlayerAttrs.DODGERATE, "Dodge Rate"},
            {PlayerAttrs.MOVESPEEDFACTOR, "Move Speed Factor"},
            {PlayerAttrs.PICKUPRANGEFACTOR, "Pickup Range Factor"},
        };
    }

    public static string getItemCls(ItemCls itemCls) => ItemCls2String[itemCls];
    public static string getWeaponCls(WeaponCls weaponCls) => WeaponCls2String[weaponCls];
    public static string getPlayerAttrs(PlayerAttrs playerAttrs) => PlayerAttrs2String[playerAttrs];
}
