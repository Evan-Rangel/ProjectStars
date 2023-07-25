using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pausePNL;
    [SerializeField] string menuSceneName;
    [SerializeField] Button buttonPressed;
    

    public void StartButtonPressed()
    {
        
        pausePNL.SetActive(!pausePNL.activeSelf);
        if (pausePNL.activeSelf)
        {
            buttonPressed.Select();
            Time.timeScale = 0;
        }
        else
        {
            
            Time.timeScale = 1;

        }
    }
    public void BackToMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuSceneName);
    }
    public void Continue()
    {
        Time.timeScale = 1;

    }
}
