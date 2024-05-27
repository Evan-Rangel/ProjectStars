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

    [Header("Camera")]
    [SerializeField] GameObject cam; 
    [Space]
    [Header("Gravity Settings")]
    [SerializeField]float maxGravityScale;
    [SerializeField] float gravityScaleMultiplication;
    [SerializeField]float gravityScaleCount= 2;
    [Space]
    [Header("Dash Settings")]
    [SerializeField] float dashForce;
    [Space]
    [Header("Sprite Settings")]
    [SerializeField] SpriteRenderer sprRenderer;

    bool OnTrigger;


    PlayerInteract tempInteraction;

    private void Awake() 
    {
        rb=GetComponent<Rigidbody2D>();
        inputs = UserInput.instance;
        GameManager.instance.player = this;
    }
    void Update()
    {
        cam.transform.position = new Vector3( transform.position.x, transform.position.y+2.5f, cam.transform.position.z);
        PlayerMovement();
        Jump();
        Interact();
        Dash();

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
        if (rb.velocity.x != 0) sprRenderer.flipX = (rb.velocity.x > 0) ? false : true;

        
        if (rb.velocity.y<3f && !onGround)
        {
            falling = true;
            rb.gravityScale = (gravityScaleCount < maxGravityScale) ? gravityScaleCount : maxGravityScale;
            gravityScaleCount += Time.deltaTime * gravityScaleMultiplication;
        }
    }
    void Jump()
    {
        if (inputs.JumpInput && onGround)
        {
            gravityScaleCount = 2;
            //IF corto, da error si esta parado
            rb.velocity = new Vector2((rb.velocity.x != 0) ? rb.velocity.x + (rb.velocity.x / MathF.Abs(rb.velocity.x)) * 10 : rb.velocity.x, jumpForce);
            onGround = false;
        }
        if (inputs.JumpReleased && !falling) 
        {
            rb.gravityScale = maxGravityScale;
        }
    }
    void Dash()
    {
        if (inputs.DashInput)
        { 
            rb.velocity = new Vector2((rb.velocity.x != 0) ? rb.velocity.x + (rb.velocity.x / MathF.Abs(rb.velocity.x)) * dashForce : rb.velocity.x, 0);
        }
    }

    public void SpawnPlayer(Vector2 _spawnTransform, PlayerData _playerData)
    { 
        gameObject.transform.position = _spawnTransform;
        health = _playerData.health;
        GameManager.instance.UpdateUI(health);
    }
    /////////////////////////////////////////////PARA CUANDO ESTE EN UN SPAWN Y GUARDE/////////////////////////////////////////////
    private void Interact()
    {
        if (OnTrigger && UserInput.instance.MoveInput.y > 0)
        {
            OnTrigger = false;
            tempInteraction.interactEvent.Invoke();
            //tempInteraction.newEvent.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<PlayerInteract>(out tempInteraction))
        {
            tempInteraction.triggerEnter.Invoke();
            OnTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<PlayerInteract>(out tempInteraction))
        {
            tempInteraction.triggerExit.Invoke();
            tempInteraction = null;
            OnTrigger = false;
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
