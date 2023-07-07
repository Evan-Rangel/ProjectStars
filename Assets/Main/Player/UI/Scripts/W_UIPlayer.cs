using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class W_UIPlayer : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Animator[] animators;
    [SerializeField] GameObject panelLose;
    [SerializeField] TMP_Text puntajeText;
    [SerializeField] TMP_Text lastScoreText;

    int score;
    private void Start()
    {
        panelLose.SetActive(false);
        score = 0;
        puntajeText.text = score.ToString();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animators = GetComponentsInChildren<Animator>(); 
    }
    public void SetScore(int _score)
    {
        score+=_score;
        puntajeText.text = score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        UIAnimatios();
    }

    private void UIAnimatios ()
    {
        //Pantalla de Vida
        if (_player.LifePlayer == 4)
        {
            animators[3].SetBool("Vida4", true);
            animators[2].SetBool("Vida3", true);
            animators[1].SetBool("Vida2", true);
            animators[0].SetBool("Vida1", true);
        }
        if (_player.LifePlayer == 3)
        {
            animators[3].SetBool("Vida4", false);
            animators[2].SetBool("Vida3", true);
            animators[1].SetBool("Vida2", true);
            animators[0].SetBool("Vida1", true);
        }
        if (_player.LifePlayer == 2)
        {
            animators[3].SetBool("Vida4", false);
            animators[2].SetBool("Vida3", false);
            animators[1].SetBool("Vida2", true);
            animators[0].SetBool("Vida1", true);
        }
        if (_player.LifePlayer == 1)
        {
            animators[3].SetBool("Vida4", false);
            animators[2].SetBool("Vida3", false);
            animators[1].SetBool("Vida2", false);
            animators[0].SetBool("Vida1", true);
        }
        if (_player.LifePlayer <= 0)
        {
            animators[3].SetBool("Vida4", false);
            animators[2].SetBool("Vida3", false);
            animators[1].SetBool("Vida2", false);
            animators[0].SetBool("Vida1", false);
            StartCoroutine(ActivarPanel());
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
    IEnumerator ActivarPanel()
    {
        yield return new WaitForSeconds(1.5f);
        lastScoreText.text = score.ToString();
        panelLose.SetActive(true);
        Time.timeScale = 0;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("W_MainMenu");
    }
}
