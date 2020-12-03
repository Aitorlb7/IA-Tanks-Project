using System.Collections.Generic;
using System;

using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction

[Action("MyActions/ChangePoint")]
[Help("Change the target when arrives to the destination")]
public class ChangePoint : GOAction
{
    [OutParam("DistanceFromPoint")]
    public float DistancePoint;

    // Start is called before the first frame update
    public override void OnStart()
    {
        
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        int Current_Point = gameObject.GetComponent<Variables>().Current_Point;
        if (Current_Point > 6)
        {
            Current_Point = 0;
        }
        else
        {
            Current_Point++;    
        }
        DistancePoint = gameObject.GetComponent<Variables>().DistancePoint;
        gameObject.GetComponent<Variables>().Current_Point = Current_Point;
        return TaskStatus.COMPLETED;
    }
}
