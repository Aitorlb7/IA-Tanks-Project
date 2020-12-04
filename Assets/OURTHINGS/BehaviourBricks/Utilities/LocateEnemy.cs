using System.Collections.Generic;
using System;

using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


[Action("MyActions/LocateEnemy")]
[Help("Find who is the enemy and where is")]

public class LocateEnemy : GOAction
{
    [OutParam("enemyTarget")]
    [Help("Target to shoot at")]
    public GameObject enemyTarget;

    [OutParam("Ammunition")]
    public int Ammo;

    [OutParam("IsEmpty")]
    public bool IsEmpty;
    
    [OutParam("Base")]
    public GameObject Base;

    // Start is called before the first frame update
    public override void OnStart()
    {
        if (gameObject.tag == "Blue")
            enemyTarget = GameObject.FindGameObjectWithTag("Red");

        if(gameObject.tag == "Red")
            enemyTarget = GameObject.FindGameObjectWithTag("Blue");

        Base = gameObject.GetComponent<Variables>().Base;
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()

    {
        Ammo = gameObject.GetComponent<Variables>().Ammunition;
        IsEmpty = gameObject.GetComponent<Variables>().IsEmpty;

        return TaskStatus.COMPLETED;
    }
}
