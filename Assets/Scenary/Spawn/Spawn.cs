using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    string spawnCode;
   // public string GetSpawnCode() { return spawnCode; }
    SpriteRenderer spr;
    BoxCollider2D coll;
    bool triggerPlayer;
    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        spawnCode = gameObject.name;
    }
    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(1);
        coll.enabled = true;
    }
    public void PlayerEnter()
    {
        spr.color = Color.blue;
    }
    public void PlayerExit()
    {
        spr.color = Color.white;
    }
    public void ActiveSpawn(Player _player)
    { 
        GameManager.instance.spawnCode = spawnCode;
        GameManager.instance.cPlayerData = new PlayerData(_player);
        SaveManager.SaveSlotData(new SlotData(null));
        coll.enabled = false;
        StartCoroutine(EnableCollision());
    }
}
