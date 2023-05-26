using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DamageBuff")]
public class W_BulletDamagePlayer : W_PowerEffect
{
    public int amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().SetDamagePlayer(target.GetComponent<Player>().BulletDamage + amount);
    }
}
