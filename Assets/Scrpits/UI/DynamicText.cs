using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DynamicText : MonoBehaviour
{
    
    [SerializeField, TextArea] string texts;
    [SerializeField] float interval = 0.1f;

    TMP_Text text;
    string curText;

    private void Awake() {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable() {
        curText = "";
        StartCoroutine(nameof(TextDisplay));
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    IEnumerator TextDisplay() {
        for (int i = 0; i < texts.Length; i++) {
            curText += texts[i];
            text.text = curText;
            yield return new WaitForSeconds(interval);
        }
    }
}
