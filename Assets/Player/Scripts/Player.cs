using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    UserInput inputs;
    int[] stats;
    bool[] buffs;
    int currentHealth;
    bool triggerSpawn;
    string spawnCode;



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
        if(inputs.FireInput)
        {
            Debug.Log("Trigger");
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
    public void SpawnPlayer(Transform _spawnTransform)
    { 
        gameObject.transform.position = _spawnTransform.position;
    }

}
//
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
