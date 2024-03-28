using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager :MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public Transform spawnPoint;
    public string slotName;
    public Dictionary<string, SaveSlot> slots = new Dictionary<string, SaveSlot>();
    public string spawnCode;
    public Dictionary<string, Transform> spawnPoints = new Dictionary<string, Transform>();
    public string gameSceneName;

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
        foreach (var p in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            Respawn point = p.GetComponent<Respawn>();
            spawnPoints.Add(point.respawnCode, point.transform);
        }
        foreach (var p in GameObject.FindGameObjectsWithTag("Slot"))
        { 
            SaveSlot slot = p.GetComponent<SaveSlot>();
            slots.Add(p.name, slot);
        }
        if (SceneManager.GetActiveScene().name == gameSceneName)
        { 
            StartGame();
        }
    }
    void StartGame()
    { 
        player.RespawnPlayer();
    }

    public void LoadGame(SaveSlot slotData)
    { 
        slotName = slotData.slotName;
        spawnCode = slotData.respawnCode;
    }


}
[Serializable]
public class PlayerData
{
    public int[] stats;
    public bool[] buffs;
}
