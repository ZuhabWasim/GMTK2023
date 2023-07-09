using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {

        if (instance == null) { instance = this; }
        else { 
            Destroy(gameObject); 
            return; 
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        // Play("burger_king");
        Play("piano_track");
        // Play("saxophone_track");
        // Play("piano_psovod");
    }

    // If you want to play audio statically:
    // FindObjectOfType<AudioManager>().Play("AUDIO-NAME");
    public void Play (string target) {
        Sound s = Array.Find(sounds, x => x.name == target);
        if (s == null) { 
            Debug.Log("Cant find sound: " + target);
            return; 
        }
        s.source.Play();
    }
}
