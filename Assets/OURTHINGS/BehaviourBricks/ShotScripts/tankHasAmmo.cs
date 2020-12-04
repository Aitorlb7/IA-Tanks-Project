using UnityEngine;
using System;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/TankHasAmmo")]
[Help("Checks whether the tank has ammo.")]
public class tankHasAmmo : ConditionBase
{
    [InParam("Ammunition")]
    private int Ammo;

    [InParam("IsEmpty")]
    public bool IsEmpty;

    public override bool Check()
    {
        Debug.Log(Ammo);
        return IsEmpty == false;
    }
}
