using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBuff")]
public class W_SpeedBuff : W_PowerEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().SetSpeedPlayer(target.GetComponent<Player>().SpeedPlayer + amount);
    }
}
