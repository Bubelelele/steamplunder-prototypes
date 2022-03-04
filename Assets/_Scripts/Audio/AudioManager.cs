using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    public bool muted;
    
    [SerializeField] private AudioMixerGroup mixerGroup;
    
    public Sound[] sounds;

    private void Awake() {
        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    private void Start() {
        Play("ambience");
    }

    public void Play(string name) {
        if (muted) return;
        
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        
        if (sound == null) {
            //Rather do normal check instead of null conditional in order to log the name
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        sound.source.Play();
    }
    public void Stop(string name)
    {
        if (muted) return;

        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound == null)
        {
            //Rather do normal check instead of null conditional in order to log the name
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        sound.source.Stop();
    }

}
