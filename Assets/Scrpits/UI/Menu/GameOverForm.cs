using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverForm : MonoBehaviour
{
    [SerializeField] private GameObject masks;
    [SerializeField] TMP_Text Title;
    [SerializeField] private Button menuButton; 
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    // 场景被加载时Awake方法启用。
    private void Awake() {
        masks.SetActive(false);
        menuButton.onClick.AddListener(OnMenuButtonClick);  // 增加按钮的监听事件
        restartButton.onClick.AddListener(OnRestartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    // 挂载了此脚本的组件被加载时，OnEnable启用，可反复
    private void OnEnable() {
        GameEvents.GameOver += GameOver;  // 加入监听
        GameEvents.GameWin += GameWin;
    }

    private void OnDisable() {
        GameEvents.GameOver -= GameOver;  // 取消监听
        GameEvents.GameWin -= GameWin;
    }

    private void GameOver(){
        Title.text = "YOU DIED";
        Title.color = Color.red;
        masks.SetActive(true);
        EnemyManager.Instance.SlayAll();
    }

    private void GameWin(){
        EnemyManager.Instance.SlayAll();
        GameManager.Instance.playerInput.DisableAllInputs();
        WaveManager.Instance.gameObject.SetActive(false);
        EnemyManager.Instance.gameObject.SetActive(false);
        PoolManager.Instance.DeActivateAllLoots();
        StartCoroutine(WinCoroutine());
    }

    IEnumerator WinCoroutine() {
        yield return 0.5f;
        Title.text = "YOU WIN !";
        Title.color = Color.green;
        masks.SetActive(true);
    }
    

    //==================== 按钮的功能 ==================//
    private void OnMenuButtonClick(){
        // 切换场景，回到主菜单
        SceneManager.LoadScene("Menu");
    }

    private void OnRestartButtonClick(){
        // 重新加载游戏场景
        SceneManager.LoadScene("Main");
    }

    private void OnQuitButtonClick(){
        // 退出游戏
        Application.Quit();

        // 若在unity编辑器模式下则停止运行
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    
}
