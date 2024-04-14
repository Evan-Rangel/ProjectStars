using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] protected int health;
    public int GetCharHealth () { return health; }
    [SerializeField] protected string charName;
    public string GetCharName()  {return charName;} 
    [SerializeField, Range(0, 10)] protected float speed;
    [SerializeField, Range(0, 1000)] protected int jumpForce;
    [SerializeField, Range(0, 1)] protected float speedMult;
    protected Rigidbody2D rb;
    [SerializeField] protected bool onGround;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { 
            onGround = true;
        }
    }
}
