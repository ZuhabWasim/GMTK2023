using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnClickScript : MonoBehaviour
{
    public void PlayAudio(string audio_name) {
        FindObjectOfType<AudioManager>().Play(audio_name);
    }
}
