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
        Timer = 0;
    }


    public override TaskStatus OnUpdate()
    {
        Timer += Time.time;
        if(Timer >= 30f)
        {
            gameObject.GetComponent<Variables>().Ammo_Images[gameObject.GetComponent<Variables>().Ammunition].SetActive(true);
            gameObject.GetComponent<Variables>().Ammunition++;
            Timer = 0;
        }
        
        if(gameObject.GetComponent<Variables>().Ammunition >= 5)
        {
            gameObject.GetComponent<Variables>().IsEmpty = false;
            Timer = 0;
            gameObject.GetComponent<Variables>().Ammunition = 5;
        }
        return TaskStatus.COMPLETED;
    }
}
