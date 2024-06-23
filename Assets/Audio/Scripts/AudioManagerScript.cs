using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public static AudioManagerScript instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        { 
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySFX(AudioClip _clip)
    { 
        
        sfxSource.PlayOneShot(_clip);
    }

}
