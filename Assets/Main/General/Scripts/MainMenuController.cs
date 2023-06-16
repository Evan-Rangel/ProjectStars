using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] string startButtonScene;

    public void StartButton()
    {
        SceneManager.LoadScene(startButtonScene);
    }
}
