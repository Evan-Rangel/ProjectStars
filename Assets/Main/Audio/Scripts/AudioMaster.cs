using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMaster : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string volumen1 = "VolumenSound";
    public AudioClip[] playerAudios;

    //Mathf.Log(soundLevel) * 20 esta formula es para acomodar los desiveles
    //tiene que ser entre 0.001 y 1
    void Start()
    {
        audioMixer.SetFloat(volumen1, Mathf.Log(0.5f) * 20);
    }


}
