using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_PowerUp : MonoBehaviour
{
    [SerializeField]
    private W_PowerEffect powerEffect;
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.CompareTag("Player"))
        {
            powerEffect.Apply(player);
            Destroy(gameObject);
        }
        
    }
}
