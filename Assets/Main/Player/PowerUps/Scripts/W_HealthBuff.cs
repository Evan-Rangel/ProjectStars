using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HealthBuff")]
public class W_HealthBuff : W_PowerEffect
{
    public int amount;
    public override void Apply(GameObject target)
    {
        //Buscamos el script de la variable que queremos modificar, tomamos la variable y sumamos o restamos dependiendo que queremos hacer (Esto para este tipo de items)
        if (target.GetComponent<Player>().LifePlayer >= 4)
        {
            target.GetComponent<Player>().SetLifePlayer(4);
        }
        else if (target.GetComponent<Player>().LifePlayer > 0 && target.GetComponent<Player>().LifePlayer < 4)
        {
            target.GetComponent<Player>().SetLifePlayer(target.GetComponent<Player>().LifePlayer + amount);
        }
        else
        {
            target.GetComponent<Player>().SetLifePlayer(0);
        }
    }
}
