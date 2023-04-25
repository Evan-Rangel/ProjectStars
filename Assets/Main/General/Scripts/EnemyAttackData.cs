using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

using UnityEditor;

#endif
[CreateAssetMenu(fileName = "New Enemy Attack Data", menuName = "Enemy Attack Data")]

public class EnemyAttackData : ScriptableObject
{
    public enum AttackType
    {
        NONE,
        BULLET,
        LASER
    }
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
    float laserOffDuration;
    float laserAngleInit;
    float laserCastDuration;
    float laserSpeedRotation;
    float laserAngleSum;
    int laserPerWave;
    int laserDamage;
    LaserType laserType;
    List<float> laserAngles;
    bool showLaserDirections;
    bool withRotation=false;



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
    int projectilesPerWave;
    int projectileAngleInit;
    float projectileAngleSum;
    float projectileSpeed;
    float projectileCadence;
    public int GetProjectilesPerWave { get { return projectilesPerWave; } }
    public int GetProjectileAngleInit { get { return projectileAngleInit; } }
    public float GetProjectileAngleSum { get { return projectileAngleSum; } }
    public float GetProjectileSpeed { get { return projectileSpeed; } }
    public float GetProjectileCadence { get { return projectileCadence; } }

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
            int labelSize = 125;
            int valueSize = 80;

            switch (enemyAttackData.attackType)
            {
                case AttackType.NONE:
                    break;
                case AttackType.BULLET:
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectiles Per Wave: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectilesPerWave = EditorGUILayout.IntField(enemyAttackData.projectilesPerWave, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectiles Angle Init: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileAngleInit = EditorGUILayout.IntField(enemyAttackData.projectileAngleInit, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Angle Sum: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileAngleSum = EditorGUILayout.FloatField(enemyAttackData.projectileAngleSum, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Speed: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileSpeed = EditorGUILayout.FloatField(enemyAttackData.projectileSpeed, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Projectile Cadence: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.projectileCadence = EditorGUILayout.FloatField(enemyAttackData.projectileCadence, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    break;
                case AttackType.LASER:

                    enemyAttackData.laserType = (LaserType)EditorGUILayout.EnumPopup(enemyAttackData.laserType, GUILayout.MaxWidth(250));


                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Total Lasers: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.laserPerWave = EditorGUILayout.IntField(enemyAttackData.laserPerWave, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Laser Angle Init: ", GUILayout.MaxWidth(labelSize));
                    enemyAttackData.laserAngleInit = EditorGUILayout.FloatField(enemyAttackData.laserAngleInit, GUILayout.MaxWidth(valueSize));
                    GUILayout.EndHorizontal();
                    switch (enemyAttackData.laserType)
                    {
                        case LaserType.STATIC:
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
                                    enemyAttackData.laserAngles[i] = (float)EditorGUILayout.IntSlider((int)enemyAttackData.laserAngles[i], 0, 360);

                                    EditorGUILayout.EndHorizontal();
                                }
                                

                            }

                            break;
                        case LaserType.RANDOM:
                            
                            
                            break;
                        

                    }
                    break;
            }

        }
    }


#endif
    #endregion

}
