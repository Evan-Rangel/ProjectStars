using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class ValueTextScript : MonoBehaviour
{
    public void SetText(Slider slider)
    {
        TMP_Text _text= GetComponent<TMP_Text>();
        _text.text = slider.value.ToString();
    }
}
