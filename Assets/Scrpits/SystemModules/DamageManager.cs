using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : Singleton<DamageManager>
{
    float finalDamage;
    bool isCritical;

    public KeyValuePair<float, bool> GetDamage(float damage) {
        isCritical = Random.Range(0, 101) < PlayerAttr.Instance.CriticalRate;
        finalDamage = isCritical ? damage * (1 + (float)PlayerAttr.Instance.CriticalDamage / 100) : damage;
        finalDamage *= (1 + (float)PlayerAttr.Instance.DamageFactor / 100);
        return new KeyValuePair<float, bool>(finalDamage, isCritical);
    }
}
