using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WaveController : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public GameObject[] enemy;
       
        public void ActivateEnemies()
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].SetActive(true);
            }
        }
        public void DisableEnemies()
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].SetActive(false);
            }
        }
        public bool CheckEnemies()
        {
            int count=0;
            for (int i = 0; i < enemy.Length; i++)
            {
                if (!enemy[i].activeSelf)
                {
                    count++;
                }
                
            }
            if (count== enemy.Length)
            {
                return true;
            }
            return false;

        }
    }
    [SerializeField] Wave[] waves;
    int waveIndex=0;

    private static WaveController instance;
    public static WaveController Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].DisableEnemies();
        }
        NextWave();
    }
    public void CheckWave()
    {
        if (waves[waveIndex].CheckEnemies() && waveIndex < waves.Length)
        {
            waveIndex++;
            NextWave();
        }
        else if(waveIndex<waves.Length)
        {
            Debug.Log("NextLevel");
        }
    }

    void NextWave()
    {
        waves[waveIndex].ActivateEnemies();
    }
}