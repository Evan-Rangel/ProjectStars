using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_UIPlayer : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Animator[] animators;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animators = GetComponentsInChildren<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        UIAnimatios();
    }

    private void UIAnimatios ()
    {
        if (_player.LifePlayer == 3)
        {
            animators[3].SetBool("Vida4", true);
        }
        else if (_player.LifePlayer == 2)
        {
            animators[2].SetBool("Vida3", true);
        }
        else if (_player.LifePlayer == 1)
        {
            animators[1].SetBool("Vida2", true);
        }
        else if (_player.LifePlayer == 0)
        {
            animators[0].SetBool("Vida1", true);
        }
    }
}
