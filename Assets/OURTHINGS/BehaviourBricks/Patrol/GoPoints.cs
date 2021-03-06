﻿using System.Collections.Generic;
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

    [OutParam("DistanceFromPoint")]
    public float DistancePoint;

    public override void OnStart()
    {
        Agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        int Current_Point = gameObject.GetComponent<Variables>().Current_Point;
        Agent.destination = GameObject.Find("Path_Points").transform.GetChild(Current_Point).transform.position;

        DistancePoint = gameObject.GetComponent<Variables>().DistancePoint;

        return TaskStatus.COMPLETED;
    }
}
