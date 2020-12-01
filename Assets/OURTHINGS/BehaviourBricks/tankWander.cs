using System.Collections.Generic;
using System;

using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


[Action("MyActions/tankWander")]
[Help("Calculate a random postion ahead of the tank and sets destination.")]

public class tankWander : GOAction
{
    private float nextCheck;
    private float refreshWander;
    public float wanderRadius;
    private bool rotate;
    private Vector3 wanderTarget;
    private Vector3 localRandomTarget;
    public RaycastHit hitInfo;
    public LineRenderer line;
    public float lineLenght;
    public UnityEngine.AI.NavMeshAgent Agent;

    public override void OnStart()
    {
        Agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        nextCheck = 0;
        wanderRadius = 3;
        lineLenght = 6;
        line = gameObject.GetComponent<LineRenderer>();

        Debug.Log(wanderRadius);
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        checkIfWander();
        return TaskStatus.COMPLETED;
    }


    void checkIfWander()
    {
        LineRaycast();

        if (RandomWanderTarget(wanderRadius, out wanderTarget))
        {
            Agent.SetDestination(wanderTarget);
        }

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
            localRandomTarget = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, 0);
            localRandomTarget.Normalize();
            nextCheck = Time.time + 0.7f;
        }
        Vector3 worldRandomTarget = gameObject.transform.TransformPoint(localRandomTarget);
        worldRandomTarget.y = 0f;
        result = worldRandomTarget;

        return true;
    }
    void LineRaycast()
    {
        if (line.enabled)
        {
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, gameObject.transform.position + gameObject.transform.forward * lineLenght);
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hitInfo, lineLenght))
            {
                if (hitInfo.collider.gameObject.tag == "Collision") rotate = true;
            }
            else rotate = false;
        }
    }
}




