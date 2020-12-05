using System.Collections.Generic;
using System;

using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


[Action("MyActions/setNewDestination")]
[Help("Calculate a random postion ahead of the tank and sets destination.")]

public class setNewDestination : GOAction
{
    private float nextCheck;
    public float wanderRadius;
    private Vector3 localRandomTarget;
    public RaycastHit hitInfo;
    public UnityEngine.AI.NavMeshAgent Agent;


    [InParam("Rotate")]
    [Help("If raycast hit rotate the tank")]
    public bool rotate;

    [InParam("newDirection")]
    public Vector3 newDirection;


    [OutParam("WanderTarget")]
    [Help("New destination for the tank")]
    public Vector3 wanderTarget;



    public override void OnStart()
    {
        Agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        wanderRadius = 3;
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        RandomWanderTarget(wanderRadius, out wanderTarget);
        return TaskStatus.COMPLETED;
    }

    bool RandomWanderTarget(float radius, out Vector3 result)
    {
        float offset = 9f;

        if (!rotate)
        {
            localRandomTarget = new Vector3(
            UnityEngine.Random.Range(-1.0f, 1.0f), 0,
            UnityEngine.Random.Range(-1.0f, 1.0f));
            localRandomTarget.Normalize();
            localRandomTarget *= radius;
            localRandomTarget += new Vector3(0, 0, offset);
        }
        else if (rotate)
        {
            localRandomTarget = newDirection;
            localRandomTarget.Normalize();

            rotate = false;
        }
        Vector3 worldRandomTarget = gameObject.transform.TransformPoint(localRandomTarget);
        worldRandomTarget.y = 0f;
        result = worldRandomTarget;

        return true;
    }
}




