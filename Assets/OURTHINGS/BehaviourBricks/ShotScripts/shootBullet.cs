using UnityEngine;
using System.Collections.Generic;
using System;
using Pada1.BBCore;          
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


[Action("MyActions/ShootBullet")]
[Help("Calculate shot angle and fire the bullet.")]

public class shootBullet : GOAction
{
    [InParam("bulletSpeed")]
    [Help("missileSpeed needed to fire the bullet")]
    public float bulletSpeed;

    [InParam("Angle")]
    [Help("Angle needed to fire the bullet")]
    public float angle;

    [InParam("Turret")]
    [Help("Angle needed to fire the bullet")]
    public GameObject Turret;

    [InParam("Cannon")]
    [Help("Angle needed to fire the bullet")]
    public Transform Cannon;

    [InParam("Bullet")]
    [Help("Angle needed to fire the bullet")]
    public Rigidbody Bullet;

    [InParam("TotalAmmo")]
    public int TotalAmmo;

    [OutParam("Ammunition")]
    [Help("Remaining ammo")]
    public int Ammunition;

    public override void OnStart()
    {

    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        
        ShootMissile();


        return TaskStatus.COMPLETED;

    }


    void ShootMissile()
    {
        if (float.IsNaN(Math.Abs(angle)))
        {
            Debug.Log("Target out of range");
            return;
        }

        Turret.transform.Rotate(angle, 0.0f, 0.0f);

        Rigidbody missile_inst = UnityEngine.Object.Instantiate(Bullet, Cannon.position, Cannon.rotation) as Rigidbody;
        missile_inst.velocity = bulletSpeed * Cannon.forward;

        Ammunition--;

    }
}


