using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSense : Singleton<AttackSense>
{
    bool isShaking = false;

    private void OnDisable() {
        StopAllCoroutines();
    }

    public void CameraShake(float duration, float strength) {
        if (isShaking) return;
        StartCoroutine(Shake(duration, strength));
    }

    IEnumerator Shake(float duration, float strength) {
        isShaking = true;
        Vector3 originPos = transform.position;

        while (duration > 0) {
            transform.position = originPos + Random.insideUnitSphere * strength;
            duration -= Time.deltaTime;
            yield return null;
        }

        transform.position = originPos;
        isShaking = false;
    }

}
