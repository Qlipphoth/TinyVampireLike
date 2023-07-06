using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEXP : Singleton<PlayerEXP>
{
    [Header("EXP Settings")]
    [SerializeField] HUDEXPBar hudEXPBar;
    [SerializeField] public int level = 1;
    [SerializeField] public float currentEXP = 0;
    [SerializeField] public float maxEXP = 100;

    [Header("EXP SFX")]
    [SerializeField] AudioData[] levelUpSFX;
    [SerializeField] GameObject  levelUpVFX;

    // 这里将经验系统的数值与显示分开实现，显示全部交给 HUDEXPBar，经验系统只负责数值的变化

    private void Start() {
        hudEXPBar.Initialize(currentEXP, maxEXP);
    }

    // 加经验功能，捡到经验球调用
    public void AddEXP(float exp) {
        currentEXP += exp;
        hudEXPBar.UpdateStates(currentEXP, maxEXP);
    }

    /// <summary>
    /// 升级功能，改变经验系统的一些数值
    /// </summary>
    public void LevelUp() {
        AudioManager.Instance.PoolPlayRandomSFX(levelUpSFX);
        PoolManager.Release(levelUpVFX, transform.position);
        currentEXP -= maxEXP;
        maxEXP *= 1.2f;
        level++;
    }

}
