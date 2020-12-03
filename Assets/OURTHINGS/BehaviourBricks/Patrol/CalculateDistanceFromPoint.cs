using System.Collections.Generic;
using System;

using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction

[Action("MyActions/CalculateDistanceFromPoint")]
public class CalculateDistanceFromPoint : GOAction
{
    [OutParam("DistanceFromPoint")]
    public float DistancePoint;

    public override void OnStart()
    {
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        DistancePoint = gameObject.GetComponent<Variables>().DistancePoint;

        return TaskStatus.COMPLETED;
    }
}
