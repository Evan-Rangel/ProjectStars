using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsWindowedScript : MonoBehaviour
{
    Image image;
    bool windowed;
    private void Start()
    {
        image = GetComponent<Image>();

        if (PlayerPrefs.HasKey("Windowed"))
        {
            LoadWindowed();
        }
        else
        {
            SetWindowed();
        }
    }
    void LoadWindowed()
    {
        windowed = ConvertToBool(PlayerPrefs.GetInt("Windowed"));
        image.color = windowed ? Color.green : Color.red;
    }
    public void SetWindowed()
    {
        windowed = !windowed;
        image.color = windowed ? Color.green : Color.red;
        PlayerPrefs.SetInt("Windowed", ConvertToInt(windowed));
    }
    bool ConvertToBool(int _value)
    {
        return _value == 1 ? true : false;
    }
    int ConvertToInt(bool _value)
    {
        return _value == true ? 1 : 0;
    }
}
