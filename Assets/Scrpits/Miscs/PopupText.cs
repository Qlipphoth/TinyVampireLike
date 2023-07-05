using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{

    [Header("Move up")]
    [SerializeField] Vector3 moveUpVector = new Vector3(0, 1, 0);  // 字体漂浮方向
    [SerializeField] float moveUpSpeed = 2.0f;  // 字体漂浮速度
    [SerializeField] float scaleUpSpeed = 1.0f;  // 字体放大速度

    [Header("Move Down")]
    [SerializeField] Vector3 moveDownVector = new Vector3(-0.7f, 1, 0);  // 字体漂浮方向
    // [SerializeField] float moveDownSpeed = 1.0f;  // 字体漂浮速度
    // [SerializeField] float scaleDownSpeed = 1.0f;  // 字体缩小速度

    [Header("Disappear")]
    [SerializeField] float disappearSpeed = 3.0f;  // 字体消失速度
    [SerializeField] float disappearTime = 0.2f;  // 字体开始消失的时间

    [Header("Damage Color")]
    [SerializeField] Color normalColor;
    [SerializeField] Color criticalColor;

    TextMeshPro textMesh;
    Color textColor;
    float disappearTimer;  // 字体消失计时器

    private void Awake() {
        textMesh = GetComponentInChildren<TextMeshPro>();
    }

    private void Update() {
        // move up
        if (disappearTimer > 0) {
            transform.position += moveUpVector * Time.deltaTime;
            moveUpVector += moveUpVector * moveUpSpeed * Time.deltaTime;  // 加速漂浮速度
            transform.localScale += Vector3.one * scaleUpSpeed * Time.deltaTime;
        }

        // // move down
        // else {
        //     transform.position -= moveDownVector * Time.deltaTime;
        //     moveDownVector += moveDownVector * moveDownSpeed * Time.deltaTime;  // 加速漂浮速度
        //     transform.localScale -= Vector3.one * scaleDownSpeed * Time.deltaTime;
        // }

        // disappear
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0) {
                gameObject.SetActive(false);
            }
        }

    }

    public void SetText(int damage, bool isCritical) {
        textMesh.text = damage.ToString();
        if (isCritical) {
            textMesh.fontSize = 7;
            textColor = criticalColor;
        } 
        else {
            textMesh.fontSize = 5;
            textColor = normalColor;
        }
        textMesh.color = textColor;
        disappearTimer = disappearTime;
    }

}
