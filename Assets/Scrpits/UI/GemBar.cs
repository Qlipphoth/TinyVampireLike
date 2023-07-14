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

    private void OnEnable() {
        SetGemNum(PlayerAttr.Instance.GemNum);
    }

    public IEnumerator UpdateGemNumCoroutine() {
        SetGemNum(PlayerAttr.Instance.GemNum);
        yield return null;
    }

    public void Initialize() {
        SetGemNum(PlayerAttr.Instance.GemNum);
    }

    private void SetGemNum(int gemNum) => gemNumText.text = gemNum.ToString();
    
}
