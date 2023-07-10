using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttrs : MonoBehaviour
{
    [SerializeField] protected GameObject itemAttrClausePrefab;
    [SerializeField] protected GameObject specialInfoPrefab;

    public void ClearClauses() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void genClause(ItemEffect effect) {
        // Generate clauses
        GameObject clause = Instantiate(itemAttrClausePrefab, transform);
        clause.GetComponent<ItemAttrClause>().SetAttrClause(effect);
    }

    public void genClause(List<ItemEffect> effects) {
        // Generate clauses
        foreach (var effect in effects) {
            genClause(effect);
        }
    }

    public void genSpecialInfo(string specialInfo) {
        GameObject info = Instantiate(specialInfoPrefab, transform);
        info.GetComponentInChildren<TMPro.TMP_Text>().text = specialInfo;
    }
    
}
