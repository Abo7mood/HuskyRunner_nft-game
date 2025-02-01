using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager Instance;
    public float soundSpeed;
    public Sprite Muted;
    public Sprite UnMuted;
    public bool isMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.Volume;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.AwakePlay;
        }
        PlaySound("Music");
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            if (isMuted) GameObject.Find("Mute Icon").GetComponent<Image>().sprite = Muted;

            StartCoroutine(VolumeDown(sounds[8].source));
        }
        if (scene.buildIndex == 1)
        {
            StartCoroutine(VolumeUp(sounds[8].source));
        }

    }
    public void MuteMusic()
    {
        if (isMuted)
        {
            PlaySound("Music");
            GameObject.Find("Mute Icon").GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else
        {
            StopSound("Music");
            GameObject.Find("Mute Icon").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

        }
        isMuted = !isMuted;
    }
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }
    IEnumerator VolumeUp(AudioSource sound)
    {
        while (sound.volume < 0.25)
        {
            sound.volume = Mathf.Lerp(sound.volume, .3f, soundSpeed);
            yield return null;
        }
        sound.volume = .3f;
        yield return null;
    }
    IEnumerator VolumeDown(AudioSource sound)
    {
        while (sound.volume > 0.1f)
        {
            sound.volume = Mathf.Lerp(sound.volume, 0.05f, soundSpeed);
            yield return null;
        }
        sound.volume = 0.05f;
        yield return null;
    }
}
