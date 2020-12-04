using System.Collections.Generic;
using System;

using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction

[Action("MyActions/GoToBase")]
public class GoToBase : GOAction
{
    public UnityEngine.AI.NavMeshAgent Agent;

    public override void OnStart()
    {
        Agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        Agent.destination = gameObject.GetComponent<Variables>().Base.transform.position;

        return TaskStatus.COMPLETED;
    }
}
