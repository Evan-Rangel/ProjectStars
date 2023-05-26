using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HealthMaxBuff")]
public class W_HealthMaxBuff : W_PowerEffect
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().SetLifePlayer(4);
    }
}
