using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pausePNL;
    [SerializeField] string menuSceneName;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePNL.SetActive(!pausePNL.activeSelf);
            if (pausePNL.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;

            }
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
