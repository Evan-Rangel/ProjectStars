using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [Header("Stats Settings")]
    [SerializeField] protected int health;
    public int GetCharHealth () { return health; }
    [SerializeField] protected string charName;
    public string GetCharName()  {return charName;}
    [SerializeField] protected CharacterD charD;
    [Space]
    [Header("Speed Settings")]
    [SerializeField, Range(0, 10)] protected float speed;
    [SerializeField, Range(0, 20)] protected float jumpForce;
    [SerializeField, Range(0, 1)] protected float speedMult;
    protected Rigidbody2D rb;
    [Space]
    [Header("Jump Settings")]
    [SerializeField] protected bool onGround;
    protected bool falling = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { 
            onGround = true;
            falling = false;
            rb.gravityScale= 1;
        }
    }
}
