using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    public AudioSource music;
    public AudioSource nightMusic;
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

    public void PlayMusic() {
        StartCoroutine(TransitionToDayMusic(5f));
    }

    public void PlayNightMusic() {
        StartCoroutine(TransitionToNightMusic(5f));
    }

    IEnumerator TransitionToNightMusic(float time) {
        nightMusic.volume = 0;
        float curTime = 0;
        while (curTime < time) {
            nightMusic.volume = Mathf.Lerp(0, 1, curTime / time);
            music.volume = Mathf.Lerp(1, 0, curTime / time);
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        nightMusic.volume = 1;
        music.volume = 0;
    }

    IEnumerator TransitionToDayMusic(float time) {
        music.volume = 0;
        float curTime = 0;
        while (curTime < time) {
            music.volume = Mathf.Lerp(0, 1, curTime / time);
            nightMusic.volume = Mathf.Lerp(1, 0, curTime / time);
            curTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        music.volume = 1;
        nightMusic.volume = 0;
    }
}
