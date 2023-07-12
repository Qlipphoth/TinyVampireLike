using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsumeGemBtn : MonoBehaviour
{
    public Button consumeGemBtn;
    [SerializeField] int consumeGemNum;
    [SerializeField] TMP_Text consumeGemNumText;

    public void Initialize(int consumeGemNum) {
        this.consumeGemNum = consumeGemNum;
        consumeGemNumText.text = consumeGemNum.ToString();
        IsGemEnough();
    }

    public void IsGemEnough() {
        if (PlayerAttr.Instance.GemNum < consumeGemNum) {
            consumeGemBtn.interactable = false;
            consumeGemNumText.color = Color.red;
        }
        else {
            consumeGemBtn.interactable = true;
            consumeGemNumText.color = Color.white;
        }
    }

}
