using UnityEngine;
using System;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/TankHasNoAmmo")]
[Help("Checks whether the tank has no ammo.")]
public class HasNoAmmo : ConditionBase
{
    [InParam("IsEmpty")]
    public bool IsEmpty;

    public override bool Check()
    {
        return IsEmpty == true;
    }
}
