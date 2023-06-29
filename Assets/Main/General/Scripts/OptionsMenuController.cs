using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public void VeryLowQuality()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void LowQuality()
    {
        QualitySettings.SetQualityLevel(2);
    }
    public void MediumQuality()
    {
        QualitySettings.SetQualityLevel(3);
    }
    public void HighQuality()
    {
        QualitySettings.SetQualityLevel(4);
    }
    public void VeryHighQuality()
    {
        QualitySettings.SetQualityLevel(5);
    }


    public void HDResolution()
    {
        Screen.SetResolution(1280, 720, true);
    }
    public void FullHDResolution()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void QuadHDResolution()
    {
        Screen.SetResolution(2560, 1440, true);
    }
    public void UltraHDResolution()
    {
        Screen.SetResolution(3840, 2160, true);
    }
}
