using System.Collections.Generic;
using System;

using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction

[Action("MyActions/GoPoints")]
[Help("Follow path points")]
public class GoPoints : GOAction
{
    public UnityEngine.AI.NavMeshAgent Agent;

    private int Current_Point = 0;

    public override void OnStart()
    {
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {  
        float DistancePoint = Vector3.Distance(gameObject.transform.position, gameObject.GetComponent<Variables>().Path_Points[Current_Point].transform.position);

        Agent.destination = gameObject.GetComponent<Variables>().Path_Points[Current_Point].transform.position;

        if (DistancePoint < 1)
        {
            Current_Point++;

            if (Current_Point >= gameObject.GetComponent<Variables>().Path_Points.Count)
            {
                Current_Point = 0;
            }
        }
        return TaskStatus.COMPLETED;
    }
}
