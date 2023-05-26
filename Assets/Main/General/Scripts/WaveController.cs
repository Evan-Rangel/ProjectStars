using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WaveController : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public enum WaveDifficult
        {
            EASY,
            NORMAL,
            HARD
        }

        public List<GameObject> enemy;
        WaveDifficult waveDifficult;

        Vector2[] enemypostitions =
       {
            new Vector2(0,1.5f), //Center Pos
            new Vector2(-2.5f,2), //Left Pos
            new Vector2(2.5f,2)  //Right Pos

        };

        //Constructor
        public Wave(int _waveSize)
        {
            enemy = new List<GameObject>();
            Debug.Log(_waveSize);
            for (int i = 0; i < _waveSize; i++)
            {
                GameObject _enemy = InstanceW.RequestEnemy();

                _enemy.transform.position = new Vector2(0,9);
                Debug.Log("Llega");
                enemy.Add(_enemy);
                RandomEnemies();
            }
        }

        public void RandomEnemies()
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].GetComponent<EnemyController>().SetEnemyData(instanceW.RandomEnemy(enemy.Count));
            }
            //instanceW.MoveEnemies();
        }

        public void ActivateEnemies()
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                Debug.Log(i);
                enemy[i].SetActive(true);
            }
        }

        public void DisableEnemies()
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].SetActive(false);
            }
        }

        public bool CheckEnemies()
        {
            int count = 0;
            for (int i = 0; i < enemy.Count; i++)
            {
                if (!enemy[i].activeSelf)
                {
                    count++;
                }
            }
            if (count == enemy.Count)
            {
                return true;
            }
            return false;
        }
        public IEnumerator MoveEnemiesToPosition()
        {
            yield return new WaitForSeconds(Time.deltaTime);
            for (int i = 0; i < enemy.Count; i++)
            {
                if (enemy.Count != 2)
                {
                    enemy[i].transform.position = Vector2.Lerp(enemy[i].transform.position, enemypostitions[i], Time.deltaTime);
                }
                else
                {
                    enemy[i].transform.position = Vector2.Lerp(enemy[i].transform.position, enemypostitions[i + 1], Time.deltaTime);
                }
            }

            int index = 0;
            for (int i = 0; i < enemy.Count; i++)
            {
                if (enemy.Count != 2 && Vector2.Distance(enemy[i].transform.position, enemypostitions[i]) < 0.1f)
                {
                    index++;
                }
                else if(Vector2.Distance(enemy[i].transform.position, enemypostitions[i + 1]) < 0.1f)
                {
                    index++;
                }
            }
            if (index != enemy.Count)
            {
                instanceW.MoveEnemies();
            }
            else
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    enemy[i].GetComponent<EnemyController>().ActiveComponents();
                }
            }
        }
    }


    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<Wave> waves;
    [SerializeField] List<EnemyData> enemiesData;

    [SerializeField] int randomWaveDifficult=1;

    [SerializeField] bool randomizerLevel;
    [SerializeField] List<GameObject> enemiesList;

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
        if (randomizerLevel)
        {
            waves = new List<Wave>();
            enemiesList = new List<GameObject>();
            RandomizerLevel();
        }
        NextWave();
    }

    void RandomizerLevel()
    {
        waves.Add(new Wave(randomWaveDifficult));
        MoveEnemies();
    }
    public EnemyData RandomEnemy(int difficulty)
    {
        int rand = UnityEngine.Random.Range(0,enemiesData.Count);
        while ((int)enemiesData[rand].GetEnemyDifficulty != difficulty )
        {
            rand = UnityEngine.Random.Range(0, enemiesData.Count);
        }
        return enemiesData[rand];
    }

    private void AddEnemyToPool()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemiesList.Add(enemy);
        enemy.transform.parent = transform;
        //enemy.SetActive(false);
    }

    public GameObject RequestEnemy()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            if (!enemiesList[i].activeSelf)
            {
                enemiesList[i].SetActive(true);
                return enemiesList[i];
            }
        }
        AddEnemyToPool();
        enemiesList[enemiesList.Count - 1].SetActive(true);
        //Debug.Log(enemiesList.Count);
        return enemiesList[enemiesList.Count - 1];
    }

    public void MoveEnemies()
    {
        StartCoroutine(waves[waveIndex].MoveEnemiesToPosition());
    }


    public void CheckWave()
    {
        
        if (randomizerLevel )
        {
            if (waves[waveIndex].CheckEnemies())
            {
                RandomizerLevel();
            }   
            return;
        }

        if (waves[waveIndex].CheckEnemies() && waveIndex < waves.Count)
        {
            waveIndex++;
            NextWave();
        }
        else if(waveIndex== waves.Count - 1)
        {
            Debug.Log("NextLevel");
        }
    }

    void NextWave()
    {
        if (waveIndex< waves.Count)
        {
            waves[waveIndex].ActivateEnemies();
        }
    }
}