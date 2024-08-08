using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource sound;
    public static AudioManager instance = null;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioManager();
            }
            return instance;
        }
    }
    private AudioManager() { }

    public void PlayAudio(AudioClip audio, bool loop=false) {
        sound.clip = audio;
        sound.loop = loop;
        sound.Play();
    }

    public void StopAudio() {
        if (sound.isPlaying) {
            sound.Stop();
        }
    }

}
