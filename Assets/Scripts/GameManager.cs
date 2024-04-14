using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameManager :MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public string slotName;
    public string spawnCode;
    public Dictionary<string, Transform> spawnPoints = new Dictionary<string, Transform>();
    public string gameSceneName;
    public string menuSceneName;

    public Canvas SlotsCanvas;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name == gameSceneName)
        {
            foreach (var p in GameObject.FindGameObjectsWithTag("Spawn"))
            {
                Respawn point = p.GetComponent<Respawn>();
                spawnPoints.Add(point.name, point.transform);
            }
            //Obtiene las variables del slot
            LoadGame();

        }
    }

    //IMPORTANTE!!!: EN ESTA FUNCION SE SETEAN TODAS LAS VARIABLES DEL JUEGO./////////////////////////////////////////////
    public void LoadGame()
    {
        UpdateUI(PlayerPrefs.GetInt("Health"));
        if (PlayerPrefs.GetString("SaveExist")=="True")
        {
            spawnCode = SaveManager.currentSaveSlot.respawnCode;
            Transform spawnTransform= GameObject.Find(spawnCode).GetComponent<Transform>();
           // spawnPoints.TryGetValue(spawnCode, out spawnTransform);
            //Spawnea al player y tambien se setearan todas las variables de el.
            player.SpawnPlayer(spawnTransform);
        }

    }
    public void UpdateUI(int _health)
    {
        UIPanel UiPanel= GameObject.FindWithTag("UIPanel").GetComponent<UIPanel>();
        UiPanel.healthTEXT.text = _health.ToString();
    }
}
