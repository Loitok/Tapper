using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager snd;

    private AudioSource audioSrc;

    private AudioClip[] Sounds;

    private int randomSounds;

    void Start()
    {
        snd = this;
        audioSrc = GetComponent<AudioSource>();
        Sounds = Resources.LoadAll<AudioClip>("Sounds");
    }

    public void PlaySounds()
    {
        randomSounds = Random.Range(0, 13);
        audioSrc.PlayOneShot(Sounds[randomSounds]);
    }
}
