using UnityEngine;
using System;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/TankHasAmmo")]
[Help("Checks whether the tank has ammo.")]
public class tankHasAmmo : ConditionBase
{

    private int Ammo;

    [InParam("enemyTarget")]
    [Help("Target to shoot at")]
    public GameObject enemyTarget;
    public override bool Check()
    {
        Ammo = enemyTarget.gameObject.GetComponent<Variables>().Ammunition;
        Debug.Log(Ammo);
        if (Ammo > 0)
        {
            Ammo--;
            enemyTarget.gameObject.GetComponent<Variables>().Ammunition = Ammo;
            return true;
            
        }
        else if (Ammo <= 0)
        {
            return false;
            
        }
        return false;
    }
}
