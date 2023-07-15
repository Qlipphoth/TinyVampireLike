using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private Material _material;
    private Vector2 _movement;
    public Vector2 speed;

    private void Start() {
        _material = GetComponent<Renderer>().material;
    }

    private void Update() {
        // y 方向的偏移量随时间而累加
        _movement += speed * Time.deltaTime;
        _material.mainTextureOffset = _movement;
    }
}
