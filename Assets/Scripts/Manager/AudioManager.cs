using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private Dictionary<string, AudioClip> audioClips;
    private AudioSource audioSource;

    // ����� �Ŵ��� �ʱ�ȭ
    public void Init()
    {
        // AudioSource �߰�
        GameObject audioObject = new GameObject("AudioManager");
        audioSource = audioObject.AddComponent<AudioSource>();
        Object.DontDestroyOnLoad(audioObject);

        // �⺻ ���� ����
        audioSource.volume = 1.0f;
        audioSource.playOnAwake = false; // ���� �� ������� �ʵ��� ����

        // AudioClip Dictionary �ʱ�ȭ
        audioClips = new Dictionary<string, AudioClip>();

        // �� ����� Ŭ�� �ε�
        LoadAudioClips("Audio/Flip", "Flip");
        LoadAudioClips("Audio/Match", "Match");
        LoadAudioClips("Audio/BGM", "BGM");
    }

    // Ư�� ������ ��� ����� Ŭ�� �ε�
    private void LoadAudioClips(string path, string key)
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(path);
        foreach (var clip in clips)
        {
            Debug.Log($"Loaded clip: {clip.name} from path: {path}");
            if (!audioClips.ContainsKey(key))
                audioClips[key] = clip; // �ߺ� ����
        }
    }


    // ȿ���� ���
    public void PlaySound(string key, float volume = 1.0f)
    {
        if (audioClips.TryGetValue(key, out var clip))
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"AudioManager: {key} ����� Ŭ���� �����ϴ�.");
        }
    }

    // BGM ��� (����)
    public void PlayBGM(string key, float volume = 1.0f)
    {
        if (audioClips.TryGetValue(key, out var clip))
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"AudioManager: {key} BGM Ŭ���� �����ϴ�.");
        }
    }

    // BGM ����
    public void StopBGM()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    // ����� Ŭ�� ĳ�� �ʱ�ȭ
    public void Clear()
    {
        audioClips.Clear();
        audioSource.Stop();
    }
}
