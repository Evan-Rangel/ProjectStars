using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControllerScript : MonoBehaviour
{
    [SerializeField] GameObject[] backgroundsParts;
    [SerializeField] int distanceBetweenParts;
    [SerializeField] int multiplySpeed;
    BackgroundData backgroundD;
    Vector2 limits;
    Vector2[] repos = { new Vector2(), new Vector2() };
    [SerializeField] Vector2[] startPos;

    bool cycleMov;
    int patternIndex;
    public int MultiplySpeed { set { multiplySpeed = value; } get { return multiplySpeed; } }
    private void Awake()
    {
        patternIndex = 0;
        cycleMov = false;
        limits = new Vector2(0, -distanceBetweenParts);
        repos[0] = new Vector2(0, distanceBetweenParts);
        repos[1] = new Vector2(0, distanceBetweenParts * 2);

    }
    private void Start()
    {
        for (int i = 0; i < backgroundsParts.Length; i++)
        {
            backgroundsParts[i].transform.position = startPos[i];
        }
    }
    private void Update()
    {
        if (cycleMov)
        {
            for (int i = 0; i < backgroundsParts.Length; i++)
            {
                PanelMovement(i);
            }
        }
    }

    void PanelMovement(int _index)
    {
        backgroundsParts[_index].transform.Translate(backgroundD.GetDirection * backgroundD.GetSpeed * Time.deltaTime*multiplySpeed);
        if (backgroundsParts[_index].transform.position.y < limits.y)
        {
            backgroundsParts[_index].transform.position = repos[0];
            CompareBackgroundType(_index);
        }
    }
    void CompareBackgroundType(int _index)
    {
        switch (backgroundD.GetBackgroundType)
        {
            case BackgroundData.BackgroundType.RANDOM:
                backgroundsParts[_index].GetComponent<SpriteRenderer>().sprite = backgroundD.GetBackgroundsSprites[Random.Range(0, backgroundD.GetBackgroundsSprites.Count)].backgroundS;

                break;
            case BackgroundData.BackgroundType.WITH_PATTERN:
                if (patternIndex >= backgroundD.GetBackgroundsSprites.Count)
                {
                    patternIndex = 0;
                }
                backgroundsParts[_index].GetComponent<SpriteRenderer>().sprite = backgroundD.GetBackgroundsSprites[patternIndex].backgroundS;
                patternIndex++;
                break;
            case BackgroundData.BackgroundType.START_AND_END:
                if (patternIndex >= backgroundD.GetBackgroundsSprites.Count)
                {
                    gameObject.SetActive(false);
                }
                
                if (patternIndex > backgroundD.GetBackgroundsSprites.Count - 2)
                {
                    backgroundsParts[_index].GetComponent<SpriteRenderer>().sprite = null;

                }
                else
                { 
                    backgroundsParts[_index].GetComponent<SpriteRenderer>().sprite = backgroundD.GetBackgroundsSprites[patternIndex].backgroundS;
                }
                patternIndex++;
                break;
            case BackgroundData.BackgroundType.PATTERN_AND_SAE:
                break;
        }
    }
    void SetFirstSprites()
    {
        if (backgroundD.GetBackgroundsSprites.Count > 1)
        {
            for (int i = 0; i < backgroundsParts.Length; i++)
            {
                backgroundsParts[i].GetComponent<SpriteRenderer>().sprite = backgroundD.GetBackgroundsSprites[i].backgroundS;
                backgroundsParts[i].GetComponent<SpriteRenderer>().sortingOrder = backgroundD.GetOrderInLayer - 3;

            }
        }
        else
        {
            for (int i = 0; i < backgroundsParts.Length; i++)
            {
                backgroundsParts[i].GetComponent<SpriteRenderer>().sprite = backgroundD.GetBackgroundsSprites[0].backgroundS;
                backgroundsParts[i].GetComponent<SpriteRenderer>().sortingOrder = backgroundD.GetOrderInLayer - 3;

            }
        }
        
    }
    public void SetProperties(BackgroundData _backgroundData)
    {
        backgroundD = _backgroundData;
        
        
        SetFirstSprites();
        cycleMov = true;
    }
    IEnumerator RandomCorr()
    {
        yield return new WaitForEndOfFrame();
    }
}
