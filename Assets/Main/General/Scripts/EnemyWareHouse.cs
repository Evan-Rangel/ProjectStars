using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWareHouse : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField]List<GameObject> enemiesToReturn;

    public List<GameObject> GetRandomEnemies(int _size) 
    {
        for (int i = 0; i < _size; i++)
        {
            int rand = Random.Range(0, enemies.Length);
            enemiesToReturn.Add(enemies[rand]);
        }
        StartCoroutine(ResetList());
        return enemiesToReturn;         
    }
    IEnumerator ResetList()
    {
        yield return new WaitForSeconds(1f);
        enemiesToReturn.Clear();
    }
}
