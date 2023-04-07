using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeMovementsScrpt : MonoBehaviour
{
    public static TypeMovementsScrpt instance;
    public static TypeMovementsScrpt Instance { get { return instance; } }

    private void Awake()
    {
        if (instance ==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}

