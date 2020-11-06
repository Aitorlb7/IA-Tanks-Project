﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Tank_Behaviour : MonoBehaviour
{
    public GameObject Missile_prefab;
    public float HP = 100f;
    public float Current_HP;
    public Image FillImage;
    public Slider SliderHealth;
    public Color FullHealth_Color;
    public Color ZeroHealth_Color = Color.black;
    public GameObject Explosion_Prefab;
    public float Speed = 2;
    public GameObject Turret; 
    public bool blue_red;
    public bool death;
    public Transform Cannon;
    public Rigidbody Bullet;
    private GameObject Enemy_Target;
    private float Missile_Speed = 20f;

    //blue
    public List<GameObject> Path_Points;
    public int Current_Point;
    public UnityEngine.AI.NavMeshAgent Agent;

    //red
    private float nextCheck;
    private float refreshWander;
    public float wanderRadius;
    public bool rotate;
    private Vector3 wanderTarget;
    private Vector3 localRandomTarget;
    public RaycastHit hitInfo;
    public LineRenderer line;
    public float lineLenght;


    // Start is called before the first frame update
    void Start()
    {
        rotate = false;
        Agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Current_HP = HP;
        line = GetComponent<LineRenderer>();

        if (blue_red)
            Enemy_Target = GameObject.FindGameObjectWithTag("Red");
        else
            Enemy_Target = GameObject.FindGameObjectWithTag("Blue");
    }

    // Update is called once per frame
    void Update()
    {
        Turret.transform.LookAt(Enemy_Target.transform);

        if (Input.GetKeyDown(KeyCode.N))
        {
            Current_HP -= 10;
            print(Current_HP);
        }

        if (blue_red)
        {
            BlueRoutine();
        }
        else
        {
            RedRoutine();
        }

        SetHealthUI();
       
    }

    private void SetHealthUI()
    {
        SliderHealth.value = Current_HP;
        FillImage.color = Color.Lerp(ZeroHealth_Color, FullHealth_Color, Current_HP / HP);

        if(Current_HP <= 0)
        {
            Explosion_Prefab.SetActive(true);
        }
    }
    //Blue Behaviour
    void BlueRoutine()
    {
        FindPathPoints();
        float DistancefromRed = Vector3.Distance(transform.position, Enemy_Target.transform.position);

        if(DistancefromRed < 100f)
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                ShootMissile();
            }
        }
        else
        {
            Patrol();
        }

        //Turret.transform.LookAt(Enemy_Target.transform);
    }

    void FindPathPoints() //Find points from the scene to follow a path
    {
        if (Path_Points.Count < GameObject.Find("Path_Points").transform.childCount)
        {
            for (int i = 0; i < GameObject.Find("Path_Points").transform.childCount; i++)
            {
                Path_Points.Add(GameObject.Find("Path_Points").transform.GetChild(i).gameObject);
            }
        }
    }

    private void Patrol()
    {
        float DistancePoint = Vector3.Distance(transform.position, Path_Points[Current_Point].transform.position);

        Agent.destination = Path_Points[Current_Point].transform.position;

        if (DistancePoint < 1)
           {
              Current_Point++;

               if (Current_Point >= Path_Points.Count)
               {
                   Current_Point = 0;
               }
            }
    }

    void GoToTank()
    {
        //transform.LookAt(GameObject.FindGameObjectWithTag("Red").transform);
        //transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        Agent.destination = Enemy_Target.transform.position;
    }

    //Red Behaviour
    void RedRoutine()
    {
        checkIfWander();
        
        Turret.transform.LookAt(Enemy_Target.transform);
    }

    void checkIfWander()
    {
        float DistancefromBlue = Vector3.Distance(transform.position, Enemy_Target.transform.position);
        refreshWander = UnityEngine.Random.Range(0.5f, 1.5f);
        LineRaycast();

        if (DistancefromBlue > 15)
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + refreshWander;
                
                if (RandomWanderTarget(wanderRadius, out wanderTarget))
                {
                    Agent.SetDestination(wanderTarget);
                }
            }
        }
    }

    bool RandomWanderTarget( float radius, out Vector3 result)
    {
        float offset = 9f;


        if(!rotate)
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
            //localRandomTarget *= radius;
            //localRandomTarget += new Vector3(0, 0, offset);

            print(localRandomTarget);
            nextCheck = Time.time + 0.7f;
        }
        Vector3 worldRandomTarget =
        transform.TransformPoint(localRandomTarget);
        worldRandomTarget.y = 0f;
        result = worldRandomTarget;

        return true;

    }
    void LineRaycast()
    {
        if(line.enabled)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * lineLenght);
            if(Physics.Raycast(transform.position,transform.forward,out hitInfo, lineLenght))
            {
                if (hitInfo.collider.gameObject.tag == "Collision")
                {
                    rotate = true;
                }
            }
            else
            {
                rotate = false;
            }
        }  
    }

    float CalculateShotAngle(float Bullet_Speed, Vector3 target) //FUNCIONA PERFECTO
    {
        float distance = Vector3.Distance(Cannon.position, target);
        double parenthesis = Physics.gravity.y * (Math.Pow(distance, 2)); //g * x^2
        double numerator = Math.Sqrt((Math.Pow(Bullet_Speed, 4) - Physics.gravity.y * (parenthesis))); //v^4 - g * (g*x^2)
        float angle = (float)((Math.Pow(Bullet_Speed, 2) - numerator)) / (Physics.gravity.y * distance); //

        angle = (float)Math.Atan(angle);
        angle *= Mathf.Rad2Deg;
        return angle;
    }

    void ShootMissile()
    {
        float X_Angle = CalculateShotAngle(Missile_Speed, Enemy_Target.transform.position);
        Turret.transform.Rotate(X_Angle, 0.0f, 0.0f);
        Turret.transform.rotation = Quaternion.Euler(X_Angle, Turret.transform.rotation.eulerAngles.y, Turret.transform.rotation.eulerAngles.z);
        print("Turret ROT" + Turret.transform.eulerAngles);
        Rigidbody missile_inst = Instantiate(Bullet, Cannon.position, Cannon.rotation);
        missile_inst.velocity = Missile_Speed * Cannon.transform.forward;
    }
}
