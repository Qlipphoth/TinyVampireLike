using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGem : Singleton<PlayerGem>
{
    [Header("Gem Settings")]
    [SerializeField] GemBar gemBar;
    [SerializeField] public int gemNum = 136;

    public void AddGem(int num) {
        gemNum += num;
        gemBar.SetGemNum(gemNum);
    }

    public IEnumerator AddGemCoroutine(int num) {
        AddGem(num);
        yield return null;
    }

}
