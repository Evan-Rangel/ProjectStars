using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    UserInput inputs;
    int[] stats;
    bool[] buffs;
    int currentHealth;
    bool triggerSpawn;

    string spawnCode;
    Spawn spawnScr;
    bool onSpawn;
    private void Awake() 
    {
        rb=GetComponent<Rigidbody2D>();
        inputs = UserInput.instance;
        GameManager.instance.player = this;
    }
    void Update()
    {
        PlayerMovement();
        Jump();
        SaveSpawn();

        if(inputs.FireInput)
        {
            health++;
            GameManager.instance.UpdateUI(health);
        }
    }
    void PlayerMovement()
    {
        Vector2 velocity = rb.velocity;

        float dirX = speed * inputs.MoveInput.x;
        velocity.x = Mathf.MoveTowards(rb.velocity.x, dirX, 1);
        rb.velocity = velocity;
    }
    void Jump()
    {
        if (inputs.JumpInput && onGround)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            onGround = false;
        }
    }
    public void SpawnPlayer(Vector2 _spawnTransform, PlayerData _playerData)
    { 
        gameObject.transform.position = _spawnTransform;
        health = _playerData.health;
        GameManager.instance.UpdateUI(health);
    }
    private void SaveSpawn()
    {
        if (onSpawn && UserInput.instance.MoveInput.y > 0)
        {
            spawnScr.ActiveSpawn();
            GameManager.instance.spawnCode = spawnScr.GetSpawnCode();
            GameManager.instance.cPlayerData = new PlayerData(this);
            SaveManager.SaveSlotData(new SlotData(null));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Spawn"))
        {
            spawnScr = collision.transform.GetComponent<Spawn>();
            spawnScr.PlayerEnter();
            onSpawn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Spawn"))
        {
            spawnScr.PlayerExit();
            onSpawn = false;
        }
    }
}
[Serializable]
public class PlayerData
{
    public int[] stats;
    public bool[] buffs;
    public string playerName;
    public int health;
    public PlayerData(Player _player)
    {
        playerName = _player.GetCharName();
        health=_player.GetCharHealth();
    }
}
