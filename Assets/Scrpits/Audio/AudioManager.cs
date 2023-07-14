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

    private AudioSource musicSource;  // 音乐播放器
    private AudioSource[] soundSources;  // 音效播放器数组
    private int soundSourceIndex = 0;  // 当前音效播放器的索引
    const float MIN_PITCH = 0.9f;  // 随机音效的最小音调
    const float MAX_PITCH = 1.1f;  // 随机音效的最大音调

    protected override void Awake() {
        base.Awake();
        
        // Music
        GameObject newMusicSource = new GameObject {name = "Music Source"};
        musicSource = newMusicSource.AddComponent<AudioSource>();
        // 将新物体设置为当前物体的子物体
        newMusicSource.transform.SetParent(transform);  

        // Sound
        soundSources = new AudioSource[sFXPoolSize];
        for (int i = 0; i < sFXPoolSize; i++) {
            GameObject newSoundSource = new GameObject("SoundSource" + i);
            soundSources[i] = newSoundSource.AddComponent<AudioSource>();
            newSoundSource.transform.parent = transform;
        }
    }

    public void PlayMusic(AudioData audioData) {
        musicSource.clip = audioData.audioClip;
        musicSource.volume = audioData.volume;
        musicSource.loop = true;
        musicSource.Play();
    }

    public IEnumerator PlayMusic(AudioData audioData, float delay) {
        yield return new WaitForSeconds(delay);
        PlayMusic(audioData);
    }

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

    public IEnumerator PoolPlayRandomSFX(AudioData audioData, float interval) {
        yield return new WaitForSeconds(interval);
        PoolPlayRandomSFX(audioData);
    }

}