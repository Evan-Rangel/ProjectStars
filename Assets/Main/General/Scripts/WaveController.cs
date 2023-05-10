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
            Debug.Log(enemy.Length);
            for (int i = 0; i < enemy.Length; i++)
            {
                Debug.Log(i);
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
                    Debug.Log("Enemy dead");
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

    private static WaveController instanceW;
    public static WaveController InstanceW { get { return instanceW; } }
    private void Awake()
    {
        if (instanceW == null)
        {
            instanceW = this;
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
            //waves[i].DisableEnemies();
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
        else if(waveIndex== waves.Length-1)
        {
            Debug.Log("NextLevel");
        }
    }

    void NextWave()
    {
        if (waveIndex< waves.Length)
        {
            waves[waveIndex].ActivateEnemies();
        }
    }
}