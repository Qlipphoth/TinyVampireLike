using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : Singleton<DamageManager>
{
    float finalDamage;
    bool isCritical;

    public KeyValuePair<float, bool> GetPlayerDamage(float damage) {
        isCritical = Random.Range(0, 101) < PlayerAttr.Instance.CriticalRate;
        finalDamage = isCritical ? damage * (1 + (float)PlayerAttr.Instance.CriticalDamage / 100) : damage;
        finalDamage *= (1 + (float)PlayerAttr.Instance.DamageFactor / 100);
        return new KeyValuePair<float, bool>(finalDamage, isCritical);
    }

    public KeyValuePair<float, bool> GetEnemyDamage(float damage) {
        if (Random.Range(0, 101) < PlayerAttr.Instance.DodgeRate) {
            return new KeyValuePair<float, bool>(0, false);
        }
        return new KeyValuePair<float, bool>(damage * (1 - (float)PlayerAttr.Instance.Armor / 100), true);
    }

    public float GetFireRate(float fireRate) {
        return fireRate * (1 - (float)PlayerAttr.Instance.AttackSpeed / 100);
    }

    public float GetFireRange(float fireRange) {
        return fireRange * (1 + (float)PlayerAttr.Instance.AttackRangeFactor / 100);
    }

}
