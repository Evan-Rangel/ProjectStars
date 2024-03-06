using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioSliderScript : MonoBehaviour
{
    [SerializeField] string channelName;
    [SerializeField] AudioMixer mainAudio;
    Slider slider;
    float volume = 0;
    private void Start()
    {
        slider = GetComponent<Slider>();
        if (PlayerPrefs.HasKey(channelName + "Volume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
    }
    public string GetChannelName()
    {
        return channelName;
    }
    public void SetVolume()
    {
        volume = slider.value;
        mainAudio.SetFloat(channelName, Mathf.Log10(volume/100) * 20);
        PlayerPrefs.SetFloat(channelName+"Volume", volume);
    }
    private void LoadVolume()
    {
        slider.value = PlayerPrefs.GetFloat(channelName + "Volume");
    }
}
