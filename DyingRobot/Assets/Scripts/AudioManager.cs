using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [NonReorderable] public Sound[] sounds;
    [NonReorderable] public Music[] musics;
    private AudioSource musicSource;
    private Coroutine coroutine;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null)
        {
            s.source.pitch = UnityEngine.Random.Range(s.pitch-0.3f,s.pitch+0.3f);
            s.source.Play();
        }
    }

    public void PlayMusic(string name)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        musicSource.loop = false;
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        Music m = Array.Find(musics, music => music.name == name);
        musicSource.clip = m.introClip;
        musicSource.Play();
        coroutine = StartCoroutine(waitForIntroEnd(m.loopingClip));
    }

    IEnumerator waitForIntroEnd(AudioClip musicLoop)
    {
        while (musicSource.isPlaying) { yield return null; }

        musicSource.clip = musicLoop;
        musicSource.Play();
        musicSource.loop = true;
        yield break;
    }
}
