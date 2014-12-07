using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public int MaxSounds = 8;

    AudioSource[] audioSources;

    void Awake()
    {
        audioSources = new AudioSource[MaxSounds];
        for (int i = 0; i < MaxSounds; ++i)
        {
            audioSources[i] = gameObject.AddComponent("AudioSource") as AudioSource;
        }
    }

    public int Play(AudioClip clip)
    {
        for (int i = 1; i < MaxSounds; ++i)
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].PlayOneShot(clip);
                return i;
            }
        }
        return -1;
    }

    public void PlayMusic(AudioClip clip)
    {
        StartCoroutine(PlayMusicProcess(clip));
    }

    IEnumerator PlayMusicProcess(AudioClip clip)
    {
        AudioSource a = audioSources[0];

        if (a.isPlaying && a.clip != clip)
        {
            while (a.volume > 0)
            {
                a.volume = Mathf.Max(a.volume - 0.1f, 0.0f);
                yield return new WaitForSeconds(0.1f);
            }
        }

        a.clip = clip;
        a.loop = true;
        a.Play();

        while (a.volume < 1)
        {
            a.volume = Mathf.Min(a.volume + 0.1f, 1.0f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
