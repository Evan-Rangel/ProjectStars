using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManagerScript : MonoBehaviour
{
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] Vector2 limits, reposition;

    [SerializeField] float speed;
    private void Start()
    {
        reposition = backgrounds[2].transform.position;
    }
    private void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.Translate(Vector2.down*speed*Time.deltaTime);
            if (backgrounds[i].transform.position.y<limits.y)
            {
                backgrounds[i].transform.position = reposition;
            }
        }
    }
}
