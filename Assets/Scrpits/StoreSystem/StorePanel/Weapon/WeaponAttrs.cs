using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttrs : MonoBehaviour
{
    [SerializeField] protected GameObject weaponAttrClausePrefab;
    [SerializeField] protected GameObject specialInfoPrefab;

    public void ClearClauses() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void genClause(WeaponEffect effect, int weaponLevel) {
        // Generate clauses
        GameObject clause = Instantiate(weaponAttrClausePrefab, transform);
        clause.GetComponent<WeaponAttrClause>().SetAttrClause(effect, weaponLevel);
    }

    public void genClause(List<WeaponEffect> effects, int weaponLevel) {
        // Generate clauses
        foreach (var effect in effects) {
            genClause(effect, weaponLevel);
        }
    }

    public void genSpecialInfo(string specialInfo) {
        GameObject info = Instantiate(specialInfoPrefab, transform);
        info.GetComponentInChildren<TMPro.TMP_Text>().text = specialInfo;
    }
}
