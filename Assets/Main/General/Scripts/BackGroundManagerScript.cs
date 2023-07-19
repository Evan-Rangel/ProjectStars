using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManagerScript : MonoBehaviour
{
    [SerializeField] BackgroundData[] backgroundsD;
    [SerializeField] GameObject backgroundPrefab;
    List<GameObject> backgrounds= new List<GameObject>();
  
    private void Start()
    {
        //indexSeconBackground = 0;
        //reposition = nebulaBackgrounds[2].transform.position;
        backgrounds = new List<GameObject>();

        for (int i = 0; i < backgroundsD.Length; i++)
        {
            backgrounds.Add(Instantiate(backgroundPrefab, transform.position, Quaternion.identity, transform));
            backgrounds[i].GetComponent<BackgroundControllerScript>().SetProperties(backgroundsD[i]);
        }
    }
}
