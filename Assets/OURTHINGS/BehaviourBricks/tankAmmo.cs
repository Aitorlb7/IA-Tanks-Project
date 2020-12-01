using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Tank has ammo?")]
[Help("Checks whether the tank has ammo.")]
public class IsCopNear : ConditionBase
{
    public override bool Check()
    {
        return true;
    }
}
