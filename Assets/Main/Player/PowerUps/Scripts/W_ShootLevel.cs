using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/ShootLevel")]
public class W_ShootLevel : W_PowerEffect
{
    public int amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().SetShootLevelPlayer(target.GetComponent<Player>().ShotLevel + amount);
    }
}
