using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttrs : MonoBehaviour
{
    [SerializeField] GameObject itemAttrClausePrefab;
    [SerializeField] GameObject specialInfoPrefab;
    public void genClauses(List<Effect> effects) {
        // Clear all clauses
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        // Generate clauses
        foreach (Effect effect in effects) {
            GameObject clause = Instantiate(itemAttrClausePrefab, transform);
            clause.GetComponent<ItemAttrClause>().SetAttrClause(effect);
        }
    }

    public void genSpecialInfo(string specialInfo) {
        GameObject info = Instantiate(specialInfoPrefab, transform);
        info.GetComponentInChildren<TMPro.TMP_Text>().text = specialInfo;
    }

}
