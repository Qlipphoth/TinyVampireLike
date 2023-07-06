using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemBar : MonoBehaviour
{
    TMP_Text gemNumText;

    private void Awake() {
        gemNumText = GetComponentInChildren<TMP_Text>();
    }

    private void Start() {
        SetGemNum(PlayerGem.Instance.gemNum);
    }

    public void SetGemNum(int gemNum) {
        gemNumText.text = gemNum.ToString();
    }
}
