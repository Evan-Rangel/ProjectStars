using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Bullet Data")]
public class BulletData : ScriptableObject
{
    [Header("Bullets Information")]
    [SerializeField] private string bulletName;
    [SerializeField] private string description;
    [Space]
    [Header("Bullet Atributes for the change of bullet Between Player and Enemies")]
    [SerializeField] private string tagName;
    [Tooltip("Este daño es unicamente para el Player")]
    [SerializeField] private int bulletDamagePlayer;//Esto es provicional
    [SerializeField] UnityEditor.Animations.AnimatorController animator;
    [SerializeField] private Sprite sprite;

    public string BulletName { get { return bulletName; } }
    public string Description { get { return description; } }
    public string TagName { get { return tagName; } }
    public int BulletDamagePlayer { get { return bulletDamagePlayer; } }
    public UnityEditor.Animations.AnimatorController Animator { get { return animator; } }
    public Sprite Sprite { get { return sprite; } }
}

