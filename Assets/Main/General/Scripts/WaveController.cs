using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WaveController : MonoBehaviour
{
    [Serializable]
    public class ColorPos
    {
        public Color color;
        public Vector2 position;
        public bool posUsed;
        public ColorPos(Color _color, Vector2 _pos)
        {
            color = _color;
            position = _pos;
            posUsed = false;
        }

    }

    [Serializable]
    public class Wave
    {
        public enum WaveDifficult
        {
            EASY,
            NORMAL,
            HARD
        }

        private List<GameObject> enemy;
        public List<EnemyData> enemyD;
        public Texture2D image;
        public EnemyData bossD;
        private List<ColorPos> colorPos;
        List<Vector2> targetPositions;
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
            if (_waveSize == 4)
            {
                enemy = new List<GameObject>();
                GameObject _enemy = InstanceW.RequestEnemy();
                _enemy.transform.position = new Vector2(0, 9);
                enemy.Add(_enemy);
            }
            else 
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
        }
        public void BossEnemy()
        {
            enemy[0].GetComponent<EnemyController>().SetEnemyData(bossD);
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
            enemy = new List<GameObject>();
            colorPos = new List<ColorPos>();
            targetPositions = new List<Vector2>();

            for (int y = 0; y < image.height; y++)
            {
                for (int x = 0; x < image.width; x++)
                {
                    GetPixelColor(x, y);
                }
            }

            foreach (EnemyData _enemyD in enemyD)
            {
                SetEnemyPositions(_enemyD);
            }
            instanceW.MoveEnemies();
        }

        public IEnumerator MoveEnemyToPos()
        {
            yield return new WaitForSeconds(Time.deltaTime);

            for (int i = 0; i < enemy.Count; i++)
            {
                Vector2 enemyPos= enemy[i].transform.position;
                enemyPos = Vector2.Lerp(enemyPos, targetPositions[i], Time.deltaTime*2.5f);
                enemy[i].transform.position = enemyPos;
            }

            int index=0;
            for (int i = 0; i < enemy.Count; i++)
            {
                if (Vector2.Distance(enemy[i].transform.position, targetPositions[i]) < 0.1f)
                {
                    index++;
                }
            }
            if (index!=enemy.Count)
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
        void SetEnemyPositions(EnemyData _enemyD)
        {
            GameObject enemyR = instanceW.RequestEnemy();
            enemy.Add(enemyR);
            foreach (ColorPos _colorPos in colorPos)
            {
                if (_enemyD.GetColor == _colorPos.color && !_colorPos.posUsed)
                {
                    _colorPos.posUsed = true;
                    enemyR.transform.position = new Vector2(0, 9);

                    targetPositions.Add(SetPosition(_colorPos.position));
                    
                    enemyR.GetComponent<EnemyController>().SetEnemyData(_enemyD);
                    return;
                }
            }
        }
        
        Vector2 SetPosition(Vector2 _pixelPos)
        {
            Vector2 worldPos = (_pixelPos / 10) - new Vector2(3,5) ;
            //Debug.Log(_pixelPos + " _ " + worldPos);
            return worldPos;
        }
        void GetPixelColor(int x, int y)
        {
            Color pixelColor = image.GetPixel(x, y);
            if (pixelColor.a == 0)
            {
                return;
            }
            colorPos.Add(new ColorPos(pixelColor, new Vector2(x,y)));

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
        else
        {
            StartCoroutine(NextWave());

        }
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
        if (waves.Count >= 14)
        {
            randomWaveDifficult = 4;
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
                enemiesList[i].SetActive(true);
                return enemiesList[i];
            }
        }
        AddEnemyToPool();
        enemiesList[enemiesList.Count - 1].SetActive(true);
        return enemiesList[enemiesList.Count - 1];
    }

    public void MoveEnemies()
    {
        if (randomizerLevel)
        {
            StartCoroutine(waves[waveIndex].MoveEnemiesToPosition());
        }
        else
        {
            StartCoroutine(waves[waveIndex].MoveEnemyToPos());
        }
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
        if (waves[waveIndex].CheckEnemies() && waveIndex < waves.Count)
        {
            waveIndex++;
            StartCoroutine(NextWave());
        }
        else if(waveIndex== waves.Count - 1)
        {
            //Debug.Log("NextLevel");
        }
    }
    IEnumerator TimeNextWave()
    {
        yield return new WaitForEndOfFrame();
        RandomizerLevel();
    }
    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(0.5f);
        if (waveIndex< waves.Count )
        {
            waves[waveIndex].ActivateEnemies();
        }
    }
}