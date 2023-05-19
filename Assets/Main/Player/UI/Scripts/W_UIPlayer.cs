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

        //Botones
        //Piu1
        if (_player.ShotLevel == 1)
        {
            animators[4].SetBool("PulsarPiu1", true);
        }
        else if (_player.ShotLevel != 1)
        {
            animators[4].SetBool("PulsarPiu1", false);
        }
        //Piu2
        if (_player.ShotLevel == 2)
        {
            animators[5].SetBool("PulsarPiu2", true);
        }
        else if (_player.ShotLevel != 2)
        {
            animators[5].SetBool("PulsarPiu2", false);
        }
        //Piu3
        if (_player.ShotLevel == 3)
        {
            animators[6].SetBool("PulsarPiu3", true);
        }
        else if (_player.ShotLevel != 3)
        {
            animators[6].SetBool("PulsarPiu3", false);
        }
        //ShootGun
        if (_player.ShotLevel == 4)
        {
            animators[7].SetBool("EncenderShootGun", true);
        }
        else if (_player.ShotLevel != 4)
        {
            animators[7].SetBool("EncenderShootGun", false); 
        }
        //DamageLevel1
        if (_player.BulletDamage == 1)
        {
            animators[8].SetBool("Damage1", true);
        }
        else if (_player.BulletDamage != 1)
        {
            animators[8].SetBool("Damage1", false);
        }
        //DamageLevel2
        if (_player.BulletDamage == 2)
        {
            animators[9].SetBool("Damage2", true);
        }
        else if (_player.BulletDamage != 2)
        {
            animators[9].SetBool("Damage2", false);
        }
        //DamageLevel3
        if (_player.BulletDamage == 3)
        {
            animators[10].SetBool("Damage3", true);
        }
        else if (_player.BulletDamage != 3)
        {
            animators[10].SetBool("Damage3", false);
        }
    }
}
