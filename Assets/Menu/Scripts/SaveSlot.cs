using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

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

    [SerializeField] Player player;
    [SerializeField] GameObject slotEmpty, slotLoad;
    [SerializeField] TMP_Text spawnName;
    [SerializeField] GameObject deleteButton;
    bool ExistLoad;
    private void Awake()
    {
        slotName = gameObject.name;
    }
    private void Start()
    {
        TransformSlotData();
    }
    public void LoadGame()
    {
        if (ExistLoad)
        {
            GameManager.instance.LoadGame(this);
        }
        SetPlayerPrefSlot();
    }
    void TransformSlotData()
    {
        SlotData _slot = SaveManager.LoadSlotData(this);

        if (_slot != null)
        {
            deleteButton.SetActive(true);
            slotEmpty.SetActive(false);
            slotLoad.SetActive(true);
            PlayerPrefs.SetString("CurrentSpawn", _slot.respawnCode);
            ExistLoad = true;
        }
        else {
            deleteButton.SetActive(false);
            slotEmpty.SetActive(true);
            slotLoad.SetActive(false);
            ExistLoad = false;
        }
    }
    public void DeleteSlotButton()
    {
        SaveManager.DeleteSlotData(this);
        TransformSlotData();
    }
    public void SetSpawnObject(string _RespawnCode)
    { 
        respawnCode = _RespawnCode;
        SaveManager.SaveSlotData(this);
    }
    public void SetPlayerPrefSlot()
    {
        SaveManager.currentSaveSlot = this;
        PlayerPrefs.SetString("CurrentSlot", slotName);
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
        slotName = slot.slotName;
        respawnCode = slot.respawnCode;
        

        // stats = slot.stats;
        // buffs = slot.buffs;

    }
}