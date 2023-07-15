using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// 主菜单功能实现
public class MenuForm : MonoBehaviour
{
    //========================= 实现选择并选中的二次操作功能 ====================//
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] AudioData mainMenuBGM;  // 背景音乐
    [SerializeField] AudioData fingerSnapAudioData;  // 选中音效
    [SerializeField] AudioData selectAudioData;  // 选择音效

    // 在这里，=> 表示 lambda 表达式将方法的返回值作为属性值返回。
    private Toggle currentSelection => toggleGroup.GetFirstActiveToggle(); 
    private Toggle onToggle;  // 目前选择的开关

    // 这里注意 Awake 和 Start，使用 Awake 时会出现空指针的错误。
    private void Start() {
        var toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        foreach (var toggle in toggles){
            // 缺省值，避免写多个函数
            toggle.onValueChanged.AddListener( _ => OnToggleValueChanged(toggle));
        }

        currentSelection.onValueChanged?.Invoke(true);  // 刚进入界面就触发事件  
        // AudioManager.Instance.PlayMusic(mainMenuBGM);  // 播放背景音乐
        StartCoroutine(AudioManager.Instance.PlayMusic(mainMenuBGM, 0.3f));
    }

    private void OnToggleValueChanged(Toggle toggle){
        if (onToggle == currentSelection){
            
            AudioManager.Instance.PoolPlaySFX(fingerSnapAudioData);  // 播放选中音效

            switch (toggle.name){
                case "GameStart":
                    SceneManager.LoadScene("Main");
                    break;
                case "Settings":
                    // load
                    break;
                case "Sponsor":
                    // load
                    break;
                case "Quit":
                    Application.Quit();
                    // 若在unity编辑器模式下则停止运行
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #endif
                    break;
                default:
                    throw new UnityException("Toggle name is Invalid!");
            }
            return;
        }
        if (toggle.isOn){
            onToggle = toggle;
            // 选中开关设置黄色
            onToggle.transform.Find("Label").GetComponent<TMP_Text>().color = Color.yellow;

            AudioManager.Instance.PoolPlaySFX(selectAudioData);  // 播放选择音效

        } 
        else{
            onToggle.transform.Find("Label").GetComponent<TMP_Text>().color = Color.white;
        }
    }
}
