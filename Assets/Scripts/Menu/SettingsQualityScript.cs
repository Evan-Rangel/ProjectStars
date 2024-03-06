using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsQualityScript : MonoBehaviour
{
    string[] qualityNames;
    TMP_Dropdown dropdown;
    private void Start()
    {
        qualityNames = QualitySettings.names;
        dropdown = GetComponent<TMP_Dropdown>();
        foreach (var quality in qualityNames)
        {
            TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData();
            newOption.text = quality;
            dropdown.options.Add(newOption);
        }
        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            LoadQuality();
        }
        else
        {
            SetQuality();
        }
    }
    void LoadQuality()
    {
        dropdown.value = PlayerPrefs.GetInt("QualityLevel");
    }
    public void SetQuality()
    {
        int qualityIndex = dropdown.value;
        QualitySettings.SetQualityLevel(qualityIndex, true);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }
}
