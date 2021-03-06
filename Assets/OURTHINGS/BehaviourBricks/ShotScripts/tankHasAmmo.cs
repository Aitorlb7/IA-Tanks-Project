﻿using UnityEngine;
using System;
using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/TankHasAmmo")]
[Help("Checks whether the tank has ammo.")]
public class tankHasAmmo : ConditionBase
{
    [InParam("IsEmpty")]
    public bool IsEmpty;

    public override bool Check()
    {
        return IsEmpty == false;
    }
}
