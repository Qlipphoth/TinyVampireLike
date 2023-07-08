using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsPanel : Singleton<PlayerStatsPanel>
{
    [SerializeField] Transform playerStatsClausesParent;
    [SerializeField] GameObject playerStatsClausePrefab;
    
    List<int> playerAttrs; 

    private void OnEnable() {
        RefreshStatsPanel();
    }

    void genStatClause(EnumAttrs.PlayerAttrs attr, int value) {
        GameObject playerStatsClause = Instantiate(playerStatsClausePrefab, playerStatsClausesParent);
        playerStatsClause.GetComponent<StatClause>().SetStatClause(attr, value);
    }

    public void RefreshStatsPanel() {
        foreach (Transform child in playerStatsClausesParent) {
            Destroy(child.gameObject);
        }
        playerAttrs = PlayerAttr.Instance.GetPlayerAttrs();
        for (int i = 0; i < playerAttrs.Count; i++) {
            genStatClause((EnumAttrs.PlayerAttrs)i, playerAttrs[i]);
        }
    }

}
