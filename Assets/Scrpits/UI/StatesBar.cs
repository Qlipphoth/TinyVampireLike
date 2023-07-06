using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 头顶状态条的基类，提供基础的初始化和更新方法
/// </summary>
public class StatesBar : MonoBehaviour
{
    [SerializeField] protected Image fillImageBack;
    [SerializeField] protected Image fillImageFront;
    [SerializeField] float fillSpeed = 0.1f;
    [SerializeField] bool delayFill = true;  // 血条 buffer 是否延迟变化
    [SerializeField] float fillDelay = 0.5f; 

    protected float currentFillAmount;
    protected float targetFillAmount;
    float t;  // 防止插值中频繁地回收
    WaitForSeconds waitForDelayFill;  // 延迟填充
    Coroutine bufferedfillingCoroutine;

    private void Awake() {
        // 只有有canvas组件的才将相机设置为主相机
        if (TryGetComponent<Canvas>(out Canvas canvas)) {
            canvas.worldCamera = Camera.main;  // canvas 绑定主摄像机
        }
        waitForDelayFill = new WaitForSeconds(fillDelay);
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    public virtual void Initialize(float currentValue, float maxValue) {
        currentFillAmount = currentValue / maxValue;
        targetFillAmount = currentFillAmount;
        fillImageBack.fillAmount = currentFillAmount;
        fillImageFront.fillAmount = currentFillAmount;
    }

    public virtual void UpdateStates(float currentValue, float maxValue) {

        targetFillAmount = currentValue / maxValue;
        if (bufferedfillingCoroutine != null) {
            StopCoroutine(bufferedfillingCoroutine);
        }
        if (currentFillAmount > targetFillAmount) {
            // if states reduce: 1. front -> target 2. back slowly reduce
            fillImageFront.fillAmount = targetFillAmount;
            bufferedfillingCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageBack));
            return;
        }
        if (currentFillAmount < targetFillAmount) {
            // if states increase: 1. back -> target 2. front slowly increase
            fillImageBack.fillAmount = targetFillAmount;
            bufferedfillingCoroutine = StartCoroutine(
                BufferedFillingCoroutine(fillImageFront)
            );
        }
    }

    IEnumerator BufferedFillingCoroutine(Image image) {
        if (delayFill) {
            yield return waitForDelayFill;
        }

        t = 0f;
        while (t < 1f) {
            t += Time.deltaTime * fillSpeed;  // 渲染相关事件，故使用 deltaTime
            currentFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, t);
            image.fillAmount = currentFillAmount;
            yield return null;  // 挂起，实现血条buffer的持续变化
        }
        if (currentFillAmount <= Mathf.Epsilon) {
            gameObject.SetActive(false);
        }
    }

    public void EmptyStates() {
        fillImageBack.fillAmount = 0;
        fillImageFront.fillAmount = 0;
        currentFillAmount = 0;
    }

}
