using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : PersistentSingleton<WaveManager>
{
    [Header("Wave Texts")]
    [SerializeField] TMP_Text waveNumText;
    [SerializeField] TMP_Text waveTimerText;

    int waveNum, waveTimer;
    WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);

    protected override void Awake() {
        base.Awake();
        waveNum = 0;
        waveTimer = 10;
    }

    private void OnEnable() {
        waveNumText.text = ($"Wave {++waveNum}").ToString();
        waveTimerText.text = waveTimer.ToString();
        StartCoroutine(nameof(WaveTimer));
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    // 1. 每一波的波数显示及倒计时
    IEnumerator WaveTimer() {
        while (waveTimer > 0) {
            yield return waitForOneSecond;
            waveTimer--;
            waveTimerText.text = waveTimer.ToString();
        }
        EnemyManager.Instance.SlayAll();
    }


    // 2. 倒计时完成消灭所有敌人

}
