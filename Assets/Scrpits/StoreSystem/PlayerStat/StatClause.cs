using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatClause : MonoBehaviour
{
    public TMP_Text statName;
    public TMP_Text statValue;

    public void SetStatClause(EnumAttrs.PlayerAttrs attr, int value) {
        statName.text = EnumAttrs.getPlayerAttrKey(attr);
        statValue.text = value.ToString();
        if (value < 0) statValue.color = Color.red;
    }

}
