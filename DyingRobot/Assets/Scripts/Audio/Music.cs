
using UnityEngine;

[System.Serializable]
public class Music
{
    public AudioClip introClip;
    public AudioClip loopingClip;
    public string name;
    [Range(0.0f, 1.0f)] public float volume;
    [Range(0.1f, 3.0f)] public float pitch;
    [HideInInspector] public AudioSource source;
}
