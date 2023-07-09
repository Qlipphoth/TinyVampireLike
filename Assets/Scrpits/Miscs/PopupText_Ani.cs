using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText_Ani : MonoBehaviour
{
    [Header("Damage Color")]
    [SerializeField] Color normalColor;
    [SerializeField] Color criticalColor;

    TextMeshPro textMesh;
    Color textColor;

    private void Awake() {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void SetText(int damage, bool isCritical) {
        textMesh.text = damage.ToString();
        textMesh.color = isCritical ? criticalColor : normalColor;
    }

    public void SetText(float damage, bool isCritical) {
        textMesh.text = ((int)damage).ToString();
        textMesh.color = isCritical ? criticalColor : normalColor;
    }

    void AE_Deactivate() {
        gameObject.SetActive(false);
    }
}
