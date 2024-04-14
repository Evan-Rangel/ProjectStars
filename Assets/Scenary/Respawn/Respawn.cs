using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public string respawnCode;
    SpriteRenderer spr;
    BoxCollider2D coll;
    bool triggerPlayer;
    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        respawnCode=gameObject.name;
    }
    private void Update()
    {
        SaveRespawn();
    }
    public void SaveRespawn()
    {
        if (triggerPlayer && UserInput.instance.MoveInput.y > 0)
        {
            coll.enabled = false;
            triggerPlayer = false;
            GameManager.instance.SlotsCanvas.enabled = true;
            SaveManager.currentSaveSlot.SetSpawnObject(respawnCode);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") ) 
        {
            spr.color = Color.blue;
            triggerPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            spr.color = Color.white;
            triggerPlayer = false;
        }
    }
}
