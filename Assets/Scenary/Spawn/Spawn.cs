using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    string SpawnCode;
    public string GetSpawnCode() { return SpawnCode; }
    SpriteRenderer spr;
    BoxCollider2D coll;
    bool triggerPlayer;
    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        SpawnCode = gameObject.name;
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
    public void ActiveSpawn()
    { 
        coll.enabled = false;
        StartCoroutine(EnableCollision());
    }
}
