using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : Singleton<WaveManager>
{
    [Header("Wave Texts")]
    [SerializeField] TMP_Text waveNumText;
    [SerializeField] TMP_Text waveTimerText;
    [SerializeField] GameObject waveCompleteText;

    [Header("Wave SFXs")]
    [SerializeField] AudioData wavecompleteSFX;

    [Header("Wave Settings")]
    [SerializeField] int waveNum, waveTime;

    public int WaveNum => waveNum;   
    int curWaveTimer;
    WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);

    private void OnEnable() {
        curWaveTimer = waveTime + waveNum * 5;
        waveNumText.text = ($"Wave {++waveNum}").ToString();
        waveTimerText.text = curWaveTimer.ToString();
        waveCompleteText.SetActive(false);
        StartCoroutine(nameof(WaveTimer));
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    // 1. 每一波的波数显示及倒计时
    IEnumerator WaveTimer() {
        
        while (curWaveTimer > 0) {
            yield return waitForOneSecond;
            curWaveTimer--;
            waveTimerText.text = curWaveTimer.ToString();
        }

        EnemyManager.Instance.SlayAll();
        PoolManager.Instance.DeActivateAllLoots();
        GameManager.Instance.playerInput.DisableAllInputs();

        waveCompleteText.SetActive(true);  // 通关显示
        AudioManager.Instance.PoolPlaySFX(wavecompleteSFX);  // 通关音效
        
        yield return waitForOneSecond;  
        yield return waitForOneSecond;  
        
        GameManager.Instance.OnWaveEnd();
    }


    // 2. 倒计时完成消灭所有敌人

}
