using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    UserInput inputs;
    GameManager gameManager;
    int[] stats;
    bool[] buffs;
    int currentHealth;
    bool triggerSpawn;
    string spawnCode;



    private void Awake() 
    {
        rb=GetComponent<Rigidbody2D>();
        inputs = UserInput.instance;
        gameManager = GameManager.instance;
    }
    void Update()
    {
        PlayerMovement();
        Jump();
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
    public void RespawnPlayer()
    { 
        gameObject.transform.position= gameManager.spawnPoint.position;
    }

}
