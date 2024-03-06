using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController_scr : MonoBehaviour
{
    [SerializeField, Range(0,10)] float speed;
    [SerializeField, Range(0,1000)] int jumpForce;
    [SerializeField, Range(0, 1)] float speedMult;
    Rigidbody2D rb;
    PlayerInput playerInput;
    bool onGround = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Jump();
    }

    void PlayerMovement() 
    {
        Vector2 velocity = rb.velocity;
        float dirX = speed * playerInput.actions["Move"].ReadValue<Vector2>().x;
        velocity.x = Mathf.MoveTowards(rb.velocity.x, dirX,1);
        rb.velocity = velocity;
    }

    void Jump()
    {
        if(playerInput.actions["Jump"].IsPressed() && onGround)
        {
            Debug.Log("Salto");
            rb.AddForce(new Vector2(0, jumpForce));
            onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground");
            onGround = true;
        }
    }
}
