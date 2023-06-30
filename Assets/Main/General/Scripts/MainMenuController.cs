using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] string startButtonScene;

    private void Start()
    {
        Time.timeScale = 1;

    }
    public void StartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(startButtonScene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
