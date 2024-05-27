using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{

    [System.Serializable]
    public class TestEvent : UnityEvent<string> { }

    public TestEvent newEvent;
    public UnityEvent interactEvent;
    public UnityEvent triggerEnter;
    public UnityEvent triggerExit;

    public UnityEvent[] testEvents; 
}
