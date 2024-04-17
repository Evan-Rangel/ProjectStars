using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using Mono.Cecil.Cil;
public class SaveSlot : MonoBehaviour
{
    //Nombre del Slot en el que se guardara (idea de 4 slots)
    [SerializeField]public string slotName { get; private set; }
    //Spawn en el que se encuentra el player, es un codigo.
    public string respawnCode { get; private set; }
    //Array que almacenara las estadisticas generales del player.
    //   0-CurrentHealth   1-MaxHealth
    //  public int[] stats { get; private set; }
    //Array que almacenara las mejoras obtenidas.
    //  public string[] buffs { get; private set; }

    [SerializeField] GameObject slotEmpty, slotLoad;
    [SerializeField] TMP_Text spawnName;
    [SerializeField] GameObject deleteButton;
    public SlotData _slot {  get; private set;}
    bool ExistLoad; 
    private void Awake()
    {
        slotName = gameObject.name;
    }
    private void Start()
    {
        TransformSlotData();
    }
    public void LoadGameScene()
    {
        /*
                
        */
        PlayerPrefs.SetString("SaveExist", ExistLoad.ToString());
        PlayerPrefs.SetString("CurrentSlot",slotName);
        SceneManager.LoadScene("TestPlayer_Scene");
    }
    void TransformSlotData()
    {
        _slot = SaveManager.LoadSlotData(this);
        //Basicamente hace lo de abajo pero mas rapido
        bool slotExist = (_slot != null) ? true : false;
        deleteButton.SetActive(slotExist);
        slotEmpty.SetActive(!slotExist);
        slotLoad.SetActive(slotExist);
        ExistLoad = slotExist; 


        /*
        if (_slot != null)
        {
            deleteButton.SetActive(true);
            slotEmpty.SetActive(false);
            slotLoad.SetActive(true);
            PlayerPrefs.SetString("SaveExist", "True");
            ExistLoad = true;
        }
        else {              //PARA SETEAR LAS VARIABLES PREDEFINIDAS
            deleteButton.SetActive(false);
            slotEmpty.SetActive(true);
            slotLoad.SetActive(false);
            PlayerPrefs.SetString("SaveExist", "False");
            ExistLoad = false;
        }*/
    }
    public void DeleteSlotButton()
    {
        SaveManager.DeleteSlotData(this);
        TransformSlotData();
    }
    public void SetSpawnObject(string _RespawnCode)
    {
        Debug.Log("Guardando...");
        respawnCode = _RespawnCode;
        SaveManager.SaveSlotData(this);
    }
}
[Serializable]
public class SlotData
{
    //Nombre del Slot en el que se guardara (idea de 4 slots)
    public string slotName;
    //Spawn en el que se encuentra el player, es un codigo.
    public string respawnCode;

    public PlayerData playerData;
    //Array que almacenara las estadisticas generales del player.
    //   0-CurrentHealth   1-MaxHealth
    // public int[] stats;
    //Array que almacenara las mejoras obtenidas.
    //  public string[] buffs;

    public SlotData(SaveSlot slot)
    {
        if (slot != null)
        {
            slotName = slot.slotName;
            respawnCode = slot.respawnCode;
            playerData = (GameObject.FindWithTag("Player") != null) ? new PlayerData(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>()) : null;
        }
        else
        {
            slotName =PlayerPrefs.GetString("CurrentSlot");
            respawnCode = GameManager.instance.spawnCode;
            playerData = GameManager.instance.cPlayerData;
        }
       // playerData = new PlayerData(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>());

        // stats = slot.stats;
        // buffs = slot.buffs;
    }

    PlayerData SetPlayerData(Player _player)
    {
        return new PlayerData(_player);
    }
}