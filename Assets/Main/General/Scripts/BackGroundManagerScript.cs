using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManagerScript : MonoBehaviour
{
    



    [SerializeField] GameObject[] nebulaBackgrounds, secondBackground, thirdBackground;
    [SerializeField] Vector2 limits, reposition, middelPos;

    [SerializeField] float nebulaSpeed, secondSpeed, thirdSpeed;
    int indexSeconBackground;
    private void Start()
    {
        indexSeconBackground =0;
        reposition = nebulaBackgrounds[2].transform.position;
    }
    private void Update()
    {
        for (int i = 0; i < nebulaBackgrounds.Length; i++)
        {
            nebulaBackgrounds[i].transform.Translate(Vector2.down*nebulaSpeed*Time.deltaTime);
            if (nebulaBackgrounds[i].transform.position.y<limits.y)
            {
                nebulaBackgrounds[i].transform.position = reposition;
            }
        }

        /*   for (int i = 0; i < thirdBackground.Length; i++)
           {
               thirdBackground[i].transform.Translate(Vector2.down * thirdSpeed * Time.deltaTime);
               if (thirdBackground[i].transform.position.y < limits.y)
               {
                   thirdBackground[i].transform.position = reposition;
               }
           }*/
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartSecondBackground();
        }
    }
    public void StartSecondBackground()
    {
        StartCoroutine(StartSecondBackgroundCorr());
    }
    IEnumerator SecondBackgroundCorr()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < secondBackground.Length; i++)
        {
            secondBackground[i].transform.Translate(Vector2.down * secondSpeed * Time.deltaTime);
            if (secondBackground[i].transform.position.y < limits.y)
            {
                secondBackground[i].transform.position = reposition;
            }
        }
        StartCoroutine(SecondBackgroundCorr());
    }
    IEnumerator StartSecondBackgroundCorr()
    {
        switch (indexSeconBackground)
        {
            case 0:
                secondBackground[0].transform.Translate(Vector2.down * secondSpeed * Time.deltaTime);
                if (secondBackground[0].transform.position.y< middelPos.y)
                {

                }
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                Debug.Log("Error: indexSecondBackground.enter_to_default");
                break;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(SecondBackgroundCorr());
    }
}
