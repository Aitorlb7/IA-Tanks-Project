using UnityEngine;
using System.Collections.Generic;
using System;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


[Action("MyActions/CalculateAngle")]
[Help("Calculate shot angle.")]

public class calculateAngle : GOAction
{
    [OutParam("Angle")]
    [Help("Angle needed to fire the bullet")]
    public float angle;

    [InParam("bulletSpeed")]
    [Help("bulletSpeed needed to fire the bullet")]
    public float bulletSpeed;

    [InParam("enemyTarget")]
    [Help("Target to shoot at")]
    public GameObject enemyTarget;

    [InParam("Cannon")]
    [Help("Angle needed to fire the bullet")]
    public Transform Cannon;

    [InParam("Turret")]
    [Help("Angle needed to fire the bullet")]
    public GameObject Turret;
    public override void OnStart()
    {

    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        angle = CalculateShotAngle(bulletSpeed, enemyTarget.transform.position);

        return TaskStatus.COMPLETED;
    }


    float CalculateShotAngle(float bulletSpeed, Vector3 target) //Calculations are correct
    {
        float distance = Vector3.Distance(Cannon.position, target);

        float parenthesis = Physics.gravity.y * distance * distance; //g * x^2

        double numerator = Math.Sqrt(Math.Pow(bulletSpeed, 4) - (Physics.gravity.y * parenthesis)); //v^4 - g * (g*x^2)

        double ATangle = ((Math.Pow(bulletSpeed, 2)) - numerator) / (Physics.gravity.y * distance); //

        double angle = Math.Atan(ATangle);

        float result = (float)angle * Mathf.Rad2Deg;

        return result;
    }
}
