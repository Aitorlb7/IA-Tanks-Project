using System.Collections.Generic;
using System;

using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


[Action("MyActions/delay&LookAt")]
[Help("Delay x seconds and Tank Look at")]

public class delay_LookAt : GOAction
{
    [InParam("enemyTarget")]
    [Help("Target to shoot at")]
    public GameObject enemyTarget;

    [InParam("Turret")]
    [Help("Angle needed to fire the bullet")]
    public GameObject Turret;

    public int delay;

    [InParam("lookAt")]
    public bool lookAt;

    private int doneUpdates;
    // Start is called before the first frame update
    public override void OnStart()
    {
        doneUpdates = 0;
        delay = 50;
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if(lookAt)
        {
            Turret.transform.LookAt(enemyTarget.transform);
        }
        if (delay > 0)
        {
            ++doneUpdates;

            if (doneUpdates == delay)
            {
                doneUpdates = 0;
                return TaskStatus.COMPLETED;
            }

        }

        base.OnUpdate();
        return TaskStatus.RUNNING;

      
    }
}