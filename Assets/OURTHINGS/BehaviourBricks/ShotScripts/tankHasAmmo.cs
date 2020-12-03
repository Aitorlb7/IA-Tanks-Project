using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/TankHasAmmo")]
[Help("Checks whether the tank has ammo.")]
public class tankHasAmmo : ConditionBase
{
    [OutParam("checkAmmo")]
    [Help("bulletSpeed needed to fire the bullet")]
    public bool checkAmmo;

    [InParam("Ammunition", DefaultValue = 5)]
    public int Ammunition;

    [InParam("TotalAmmo")]
    public int TotalAmmo;

    public override bool Check()
    {



        if (Ammunition > 0)
        {
            checkAmmo = true;
        }
        else if (Ammunition <= 0)
        {
            checkAmmo = false;
            
        }

        Debug.Log(Ammunition);

        return checkAmmo;
    }
}
