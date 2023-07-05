using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频管理器，负责播放游戏中的所有音效
/// </summary>
public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;  // 音效播放器
    [SerializeField] int sFXPoolSize = 10;  // 音效池大小

    private AudioSource[] soundSources;  // 音效播放器数组
    private int soundSourceIndex = 0;  // 当前音效播放器的索引
    const float MIN_PITCH = 0.9f;  // 随机音效的最小音调
    const float MAX_PITCH = 1.1f;  // 随机音效的最大音调

    protected override void Awake() {
        base.Awake();
        
        soundSources = new AudioSource[sFXPoolSize];
        for (int i = 0; i < sFXPoolSize; i++) {
            GameObject newSoundSource = new GameObject("SoundSource" + i);
            soundSources[i] = newSoundSource.AddComponent<AudioSource>();
            newSoundSource.transform.parent = transform;
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioData"></param>
    public void PlaySFX(AudioData audioData) {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volume);  // 会覆盖掉其他声音，适合音效播放
    }
 
    /// <summary>
    /// 播放随机音调的音效，适合播放连续的普通音效，比如枪声
    /// </summary>
    /// <param name="audioData"></param>
    public void PlayRandomSFX(AudioData audioData) {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioData);
    }

    /// <summary>
    /// 从多个音效中随机播放一个
    /// </summary>
    /// <param name="audioData"></param>
    public void PlayRandomSFX(AudioData[] audioData) {
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }

    /// <summary>
    /// 从音效池中播放音效
    /// </summary>
    /// <param name="audioData"></param>
    public void PoolPlaySFX(AudioData audioData) {
        soundSources[soundSourceIndex].PlayOneShot(audioData.audioClip, audioData.volume);
        soundSourceIndex = (soundSourceIndex + 1) % sFXPoolSize;
    }

    public void PoolPlayRandomSFX(AudioData audioData) {
        soundSources[soundSourceIndex].pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PoolPlaySFX(audioData);
    }

    public void PoolPlayRandomSFX(AudioData[] audioData) {
        PoolPlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }

}