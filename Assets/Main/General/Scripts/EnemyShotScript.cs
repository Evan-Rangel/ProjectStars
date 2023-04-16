using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotScript : MonoBehaviour
{
    [SerializeField] EnemyShotData[] shotData;
    [SerializeField] GameObject projectile;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shooting(shotData[0].ProjectileAngleInit, shotData[0].ProjectilesPerWave);
        }
    }


    void Shooting(int _addAngle, float _angleStep)
    {
        float angleStep = 360/_angleStep;
        float angle = _addAngle;
        Vector2 startPoint = transform.position;

        for (int i = 0; i < shotData[0].ProjectilesPerWave; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180);
            Vector2 projectileVector = new Vector2(projectileDirXPosition, projectileDirYPosition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * shotData[0].ProjectileSpeed;

            GameObject tmpObj = Instantiate(projectile, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody2D>().velocity = projectileMoveDirection;

            angle += angleStep;
        }

    }
}
