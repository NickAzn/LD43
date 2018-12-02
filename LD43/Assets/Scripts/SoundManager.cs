using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    public AudioSource music;
    public AudioSource sfx;

    // Use this for initialization
    void Awake() {
        //Singleton that persists through scenes
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlaySfx(AudioClip clip) {
        sfx.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip) {
        music.clip = clip;
        music.Play();
    }
}
