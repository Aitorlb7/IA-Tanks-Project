using UnityEngine;
using System.Collections.Generic;
using System;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


[Action("MyActions/ReloadAmmo")]
public class ReloadAmmo : GOAction
{
    private float Timer;
    public override void OnStart()
    {
        
    }


    public override TaskStatus OnUpdate()
    {
        Timer += Time.time;
        return TaskStatus.COMPLETED;
    }
}
