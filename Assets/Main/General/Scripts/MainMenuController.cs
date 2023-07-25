using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] string startButtonScene;
    [SerializeField] PlayerInput input;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject startButton;

    bool startButtonIsPressed;
    private void Start()
    {
        startButtonIsPressed = true;
        input = GetComponent<PlayerInput>();
        Time.timeScale = 1;

    }
    private void Update()
    {
        if (input.actions["Start"].WasPressedThisFrame() && panel.activeSelf)
        {
            StartPanel();
            startButtonIsPressed = false;
        }
    }
    void StartPanel()
    {
        panel.GetComponent<Animator>().SetBool("Start", true);
        StartCoroutine(StartInputCorr());
    }
    IEnumerator StartInputCorr()
    {
        yield return new WaitForSeconds(0.3f);
        panel.SetActive(false);
        mainMenuPanel.SetActive(true);
        startButton.GetComponent<Button>().Select();

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
