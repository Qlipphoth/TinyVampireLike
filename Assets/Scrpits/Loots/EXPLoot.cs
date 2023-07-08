using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPLoot : LootItem
{
    [SerializeField] float exp = 8f;  // 提供的经验值

    protected override void PickedUp() {
        GameManager.Instance.OnGemChangedInGame(Random.Range(1, 4));
        PlayerEXP.Instance.AddEXP(exp);
        base.PickedUp();
    }
}
