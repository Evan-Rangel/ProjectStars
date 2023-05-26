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
            for (int i = 0; i < _waveSize; i++)
            {
                GameObject _enemy = InstanceW.RequestEnemy();

                _enemy.transform.position = new Vector2(0,9);
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
        }

        public void ActivateEnemies()
        {
            for (int i = 0; i < enemy.Count; i++)
            {
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
            bool nextWave;
            for (int i = 0; i < enemy.Count; i++)
            {
                if (!enemy[i].activeSelf)
                {
                    count++;
                }
            }
            if (count == enemy.Count)
            {
                nextWave = true ;
            }
            else
            {
                nextWave = false;
            }
            Debug.Log(nextWave);
            return nextWave;
        }
        public IEnumerator MoveEnemiesToPosition()
        {
            yield return new WaitForSeconds(Time.deltaTime);
            for (int i = 0; i < enemy.Count; i++)
            {
                if (enemy.Count != 2)
                {
                    enemy[i].transform.position = Vector2.Lerp(enemy[i].transform.position, enemypostitions[i], Time.deltaTime*2.5f);
                }
                else
                {
                    enemy[i].transform.position = Vector2.Lerp(enemy[i].transform.position, enemypostitions[i + 1], Time.deltaTime*2.5f);
                }
            }

            int index = 0;
            for (int i = 0; i < enemy.Count; i++)
            {

                if (enemy.Count != 2 )
                {
                    if (Vector2.Distance(enemy[i].transform.position, enemypostitions[i]) < 0.1f)
                    {
                        index++;
                    }
                }
                else 
                {
                    if(Vector2.Distance(enemy[i].transform.position, enemypostitions[i + 1]) < 0.1f)
                    {
                        index++;
                    }
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
        waveIndex = waves.Count - 1 ;
        MoveEnemies();
        if (waves.Count%5==0&&waves.Count<14)
        {
            randomWaveDifficult++;
        }
    }
    public EnemyData RandomEnemy(int difficulty)
    {
        int rand = UnityEngine.Random.Range(0,enemiesData.Count);
        //while ((int)enemiesData[rand].GetEnemyDifficulty != difficulty )
        {
          //  rand = UnityEngine.Random.Range(0, enemiesData.Count);
        }
        return enemiesData[rand];
    }

    private void AddEnemyToPool()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemiesList.Add(enemy);
        enemy.transform.parent = transform;
    }

    public GameObject RequestEnemy()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            if (!enemiesList[i].activeSelf)
            {
                Debug.Log("Activar enemigo: " + enemiesList[i].activeSelf);
                enemiesList[i].SetActive(true);
                Debug.Log("Despues: " + enemiesList[i].activeSelf);
                return enemiesList[i];
            }
        }
        AddEnemyToPool();
        enemiesList[enemiesList.Count - 1].SetActive(true);
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
                StartCoroutine(TimeNextWave());
            }   
            return;
        }
        Debug.Log("Siguiente");
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
    IEnumerator TimeNextWave()
    {
        yield return new WaitForEndOfFrame();
        RandomizerLevel();
    }
    void NextWave()
    {
        if (waveIndex< waves.Count )
        {
           // waves[waveIndex].ActivateEnemies();
        }
    }
}