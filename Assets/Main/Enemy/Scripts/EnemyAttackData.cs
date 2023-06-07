using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR

using UnityEditor;

#endif
[CreateAssetMenu(fileName = "New Enemy Attack Data", menuName = "Enemy Attack Data")]
[Serializable]
public class EnemyAttackData : ScriptableObject
{
    public enum AttackType
    {
        NONE,
        BULLET,
        LASER
    }
    //Tipos de laseres

    public enum LaserType
    {
        STATIC,
        DINAMIC,
        SWITCH,
        CUSTOM,
        RANDOM
    }

    [SerializeField] AttackType attackType;
    public AttackType GetAttackType { get { return attackType; } }

    //For laser attack type
    float laserOnDuration;
    //void SetLaserOnDuration(float _laserOnDuration) { laserOnDuration = _laserOnDuration; }

    [SerializeField, HideInInspector] float laserOffDuration;
    [SerializeField, HideInInspector] float laserAngleInit;
    [SerializeField, HideInInspector] float laserCastDuration;
    [SerializeField, HideInInspector] float laserSpeedRotation;
    [SerializeField, HideInInspector] float laserAngleSum;
    [SerializeField, HideInInspector] int laserPerWave;

    [SerializeField, HideInInspector] int laserDamage;
    [SerializeField, HideInInspector] LaserType laserType;
    [SerializeField, HideInInspector] List<float> laserAngles;
    [SerializeField, HideInInspector] bool showLaserDirections;
    [SerializeField, HideInInspector] bool withRotation= false;
    [SerializeField, HideInInspector] float laserDistance;





    public float GetLaserOnDuration { get { return laserOnDuration; } }
    public float GetLaserOffDuration { get { return laserOffDuration; } }
    public float GetLaserAngleInit { get { return laserAngleInit; } }
    public float GetLaserCastDuration { get { return laserCastDuration; } }
    public float GetLaserSpeedRotation { get { return laserSpeedRotation; } }
    public float GetLaserAngleSum { get { return laserAngleSum; } }
    public int GetLaserPerWave { get { return laserPerWave; } }
    public int GetLaserDamage { get { return laserDamage; } }
    public LaserType GetLaserType { get { return laserType; } }
    public List<float> GetLaserAngles { get { return laserAngles; } }


    //For bullet attack type
    [SerializeField, HideInInspector] int projectilesPerWave;
    [SerializeField, HideInInspector] int projectileAngleInit;
    [SerializeField, HideInInspector] float projectileAngleSum;
    [SerializeField, HideInInspector] float projectileSpeed;
    [SerializeField, HideInInspector] float projectileCadence;
    [SerializeField, HideInInspector] int projectileDamage;
    [SerializeField, HideInInspector] float projectileRotation;
    [SerializeField, HideInInspector] float projectileTimeRot;
    [SerializeField, HideInInspector] int  angleToShot;
    [SerializeField, HideInInspector] int anlgesSteps;


    public int GetProjectilesPerWave { get { return projectilesPerWave; } }
    public int GetProjectileAngleInit { get { return projectileAngleInit; } }
    public float GetProjectileAngleSum { get { return projectileAngleSum; } }
    public float GetProjectileSpeed { get { return projectileSpeed; } }
    public float GetProjectileCadence { get { return projectileCadence; } }
    public float GetProjectileRotation { get { return projectileRotation; } }
    public float GetProjectileTimeRot { get { return projectileTimeRot; } }
    public int GetAngleToShot { get { return angleToShot; } }
    public int GetAnglesSteps { get { return anlgesSteps; } }


    void NewLaserDirection()
    {
        laserAngles.Add(0);
    }


    [ContextMenu("ResetVariables()")]

    void DeleteLaser()
    {
        laserAngles.Clear();
    }

    void ResetVariables()
    {
        laserOnDuration = 0;
        laserOffDuration = 0;
        laserAngleInit = 0;
        laserCastDuration = 0;
        laserSpeedRotation = 0;
        laserAngleSum = 0;
        laserPerWave = 0;
        laserDamage = 0;
        laserAngles.Clear();



        projectilesPerWave = 0;
        projectileAngleInit = 0;
        projectileAngleSum = 0;
        projectileSpeed = 0;
        projectileCadence = 0;


    }
    
    #region Editor
#if UNITY_EDITOR

    [CustomEditor(typeof(EnemyAttackData))]
    public class EnemyShotDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //Necesario par funcionar, si se quita y se carga el unity, se reseta el inspector
            base.OnInspectorGUI();

            EnemyAttackData enemyAttackData = (EnemyAttackData)target;
            //EditorUtility.SetDirty(enemyAttackData);

            int labelSize = 150;
            int valueSize = 300;
            
            switch (enemyAttackData.attackType)
            {
                case AttackType.NONE:
                    break;
                case AttackType.BULLET:
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Damage: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileDamage = EditorGUILayout.IntSlider(enemyAttackData.projectileDamage, 1,10, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectiles Per Wave: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectilesPerWave = EditorGUILayout.IntSlider(enemyAttackData.projectilesPerWave,0, 100, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectiles Angle Init: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileAngleInit = EditorGUILayout.IntSlider(enemyAttackData.projectileAngleInit, 0, 359, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Angle Sum: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileAngleSum = EditorGUILayout.Slider(enemyAttackData.projectileAngleSum, -359, 359, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Speed: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileSpeed = EditorGUILayout.Slider(enemyAttackData.projectileSpeed, 0.01f, 5, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Cadence: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileCadence = EditorGUILayout.Slider(enemyAttackData.projectileCadence, 0.001f, 5, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Rotation: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileRotation = EditorGUILayout.Slider(enemyAttackData.projectileRotation, -100, 100, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Time Rotation: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileTimeRot = EditorGUILayout.Slider(enemyAttackData.projectileTimeRot, 0, 10, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Angle To Shot: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.angleToShot = EditorGUILayout.IntSlider(enemyAttackData.angleToShot, 0, 360, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Angle Stepst: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.anlgesSteps = EditorGUILayout.IntSlider(enemyAttackData.anlgesSteps, 0, 10, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();

                    break;
                case AttackType.LASER:

                    enemyAttackData.laserType = ((LaserType)EditorGUILayout.EnumPopup(enemyAttackData.laserType, GUILayout.MaxWidth(250)));


                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Total Lasers: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.laserPerWave = EditorGUILayout.IntField(enemyAttackData.laserPerWave, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Laser Angle Init: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.laserAngleInit = EditorGUILayout.FloatField(enemyAttackData.laserAngleInit, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Laser Cast Duration: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.laserCastDuration = EditorGUILayout.FloatField(enemyAttackData.laserCastDuration, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();

                    
                    switch (enemyAttackData.laserType)
                    {
                        case LaserType.STATIC:
                            enemyAttackData.laserSpeedRotation = 0;
                            break;
                        case LaserType.DINAMIC:
                            GUILayout.BeginHorizontal();

                            EditorGUILayout.LabelField("Laser Speed Rotation: ", GUILayout.MaxWidth(labelSize));
                            enemyAttackData.laserSpeedRotation = EditorGUILayout.FloatField(enemyAttackData.laserSpeedRotation, GUILayout.MaxWidth(valueSize));
                            GUILayout.EndHorizontal();
                            break;
                        case LaserType.SWITCH:

                            
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Laser On Duration: ", GUILayout.MaxWidth(labelSize));
                            enemyAttackData.laserOnDuration = EditorGUILayout.FloatField(enemyAttackData.laserOnDuration, GUILayout.MaxWidth(valueSize));
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Laser Of Duration: ", GUILayout.MaxWidth(labelSize));
                            enemyAttackData.laserOffDuration = EditorGUILayout.FloatField(enemyAttackData.laserOffDuration, GUILayout.MaxWidth(valueSize));
                            GUILayout.EndHorizontal();

                            enemyAttackData.withRotation = EditorGUILayout.Toggle("Whit Rotation: ", enemyAttackData.withRotation, GUILayout.MaxWidth(300));
                            if (enemyAttackData.withRotation)
                            {
                                GUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("Laser Speed Rotation: ", GUILayout.MaxWidth(labelSize));
                                enemyAttackData.laserSpeedRotation = EditorGUILayout.FloatField(enemyAttackData.laserSpeedRotation, GUILayout.MaxWidth(valueSize));
                                GUILayout.EndHorizontal();
                            }
                            else
                            {
                                enemyAttackData.laserSpeedRotation = 0;
                            }
                            
                            break;
                        case LaserType.CUSTOM:
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Laser Cast Duration: ", GUILayout.MaxWidth(labelSize));
                            enemyAttackData.laserCastDuration = EditorGUILayout.FloatField(enemyAttackData.laserCastDuration, GUILayout.MaxWidth(valueSize));
                            GUILayout.EndHorizontal();
                    
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Laser Angle Sum: ", GUILayout.MaxWidth(labelSize));
                            enemyAttackData.laserAngleSum = EditorGUILayout.FloatField(enemyAttackData.laserAngleSum, GUILayout.MaxWidth(valueSize));
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();

                            enemyAttackData.showLaserDirections = EditorGUILayout.Foldout(enemyAttackData.showLaserDirections,"Laser Directions",true);
                            if (enemyAttackData.showLaserDirections)
                            {
                                GUILayout.BeginHorizontal();
                                if (GUILayout.Button("New Laser Angle Direction", GUILayout.MaxWidth(250)))
                                {
                                    enemyAttackData.NewLaserDirection();
                                }

                                if (GUILayout.Button("Delete all Laser", GUILayout.MaxWidth(250)))
                                {
                                    enemyAttackData.DeleteLaser();
                                }
                                GUILayout.EndHorizontal();
                                for (int i = 0; i < enemyAttackData.laserAngles.Count; i++)
                                {
                                    EditorGUILayout.BeginHorizontal();

                                    EditorGUILayout.LabelField("Laser " + i + " angle: ");
                                    enemyAttackData.laserAngles[i] = EditorGUILayout.Slider(enemyAttackData.laserAngles[i], 0, 360);

                                    EditorGUILayout.EndHorizontal();
                                }
                            }
                            break;
                        case LaserType.RANDOM:
                            EditorGUILayout.LabelField("Laser Angle: ");
                            enemyAttackData.laserAngleInit = EditorGUILayout.FloatField(enemyAttackData.laserAngleInit);
                            EditorGUILayout.LabelField("Laser Distance: ");

                            break;
                    }
                    break;
            }
            EditorUtility.SetDirty(enemyAttackData);
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }
        }
    }


#endif
    #endregion
    

}
