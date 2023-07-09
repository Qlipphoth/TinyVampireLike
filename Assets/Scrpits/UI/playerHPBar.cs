using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHPBar : StatesBar
{
    WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);

    public override void Initialize(float currentValue, float maxValue) {
        base.Initialize(currentValue, maxValue);
        gameObject.SetActive(false);
    }

    IEnumerator updateHPBarCoroutine() {
        gameObject.SetActive(true);
        yield return waitForOneSecond;
        gameObject.SetActive(false);
    }
}
