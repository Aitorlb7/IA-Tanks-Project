using System.Collections.Generic;
using System;

using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


[Action("MyActions/moveToDestination")]
[Help("Move the tank to the previously set Destination.")]

public class moveToDestination : GOAction
{

    public UnityEngine.AI.NavMeshAgent Agent;


    [InParam("WanderTarget")]
    [Help("New destination for the tank")]
    public Vector3 wanderTarget;

    [InParam("enemyTarget")]
    [Help("Target to shoot at")]
    public GameObject enemyTarget;

    [InParam("Turret")]
    [Help("Angle needed to fire the bullet")]
    public GameObject Turret;


    public override void OnStart()
    {
        Agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        Debug.Log("Enemy position");
        Debug.Log(enemyTarget.transform.position);

        Debug.Log("Ally tank position");
        Debug.Log(this.gameObject.transform.position);

        Turret.transform.LookAt(enemyTarget.transform);
        Agent.SetDestination(wanderTarget);
        return TaskStatus.COMPLETED;
    }

}