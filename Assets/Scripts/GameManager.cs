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
    public SlotData cSlotData;
    public PlayerData cPlayerData;
    public static GameManager instance;
    public Player player;
    public string slotName;
    public string spawnCode;
    public Dictionary<string, Transform> spawnPoints = new Dictionary<string, Transform>();
    public string gameSceneName;
    public string menuSceneName;

    public PlayerInteract currentPlayerInteract;

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
                Spawn point = p.GetComponent<Spawn>();
                spawnPoints.Add(point.name, point.transform);
            }
            //Obtiene las variables del slot
            if (PlayerPrefs.HasKey("CurrentSlot"))
            {
                slotName = PlayerPrefs.GetString("CurrentSlot");
            }
            LoadGame();
        }
    }

    //IMPORTANTE!!!: EN ESTA FUNCION SE SETEAN TODAS LAS VARIABLES DEL JUEGO./////////////////////////////////////////////
    public void LoadGame()
    {
        //Si existen datos de guardado los agarra
        if (PlayerPrefs.GetString("SaveExist")==true.ToString())
        {
            cSlotData = SaveManager.LoadSlotData(slotName);

            //Spawnea al player y tambien se setearan todas las variables de el.
            Vector2 spawnTransform = GameObject.Find(cSlotData.respawnCode).GetComponent<Transform>().position;
            player.SpawnPlayer(spawnTransform, cSlotData.playerData);
        }
        else 
        { 
            UpdateUI(0);
        }
    }
    public void UpdateUI(int _health)
    {
        UIPanel UiPanel= GameObject.FindWithTag("UIPanel").GetComponent<UIPanel>();
        UiPanel.healthTEXT.text = _health.ToString();
    }
}
