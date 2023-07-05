using UnityEngine;

/// <summary>
/// 音频数据，包含音频剪辑和音量
/// </summary>
[System.Serializable]  // 使得该类可以在 Inspector 面板中显示
public class AudioData {
    public AudioClip audioClip;
    public float volume;
}
