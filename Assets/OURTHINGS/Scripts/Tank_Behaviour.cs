using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Tank_Behaviour : MonoBehaviour
{
    public GameObject Missile_prefab;
    private float HP = 100f;
    private float Current_HP;
    public Image FillImage;
    public Slider SliderHealth;
    public Color FullHealth_Color;
    public Color ZeroHealth_Color = Color.black;
    public GameObject Explosion_Prefab;
    public bool isBlue;
    private bool death;  
    private GameObject Enemy_Target;

    //ShootSystem
    public GameObject Turret; 
    public Transform Cannon;
    public Rigidbody Bullet;
    public float Missile_Speed;
    private float Shoot_Timer;

    //Blue --> Patrol
    public List<GameObject> Path_Points;
    private int Current_Point;
    public UnityEngine.AI.NavMeshAgent Agent;

    //Red --> Wander
    private float nextCheck;
    private float refreshWander;
    public float wanderRadius;
    private bool rotate;
    private Vector3 wanderTarget;
    private Vector3 localRandomTarget;
    public RaycastHit hitInfo;
    public LineRenderer line;
    public float lineLenght;


    // Start is called before the first frame update
    void Start()
    {
        rotate = false;
        Missile_Speed = 20.0f;
        Agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Current_HP = HP;
        line = GetComponent<LineRenderer>();
        Shoot_Timer = UnityEngine.Random.Range(2f, 4f);

        if (isBlue)
            Enemy_Target = GameObject.FindGameObjectWithTag("Red");
        else
            Enemy_Target = GameObject.FindGameObjectWithTag("Blue");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot_Timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.N))
        {
            Current_HP -= 10;
            print(Current_HP);
        }
        if (isBlue)
            BlueRoutine();

        else
            RedRoutine();

        if (Input.GetKeyDown(KeyCode.S))
        {
            Agent.isStopped = !Agent.isStopped;
        }

        Turret.transform.LookAt(Enemy_Target.transform);


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

        if(Shoot_Timer <= 0) ShootMissile();

        Patrol();
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

    //Red Behaviour
    void RedRoutine()
    {

        if (Shoot_Timer <= 0) ShootMissile();

        checkIfWander();

        Turret.transform.LookAt(Enemy_Target.transform);
    }

    void checkIfWander()
    {
        LineRaycast();
        refreshWander = UnityEngine.Random.Range(0.5f, 1.5f);
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + refreshWander;

            if (RandomWanderTarget(wanderRadius, out wanderTarget))
            {
                Agent.SetDestination(wanderTarget);
            }
            else
                print("No Wander target found");
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
                if (hitInfo.collider.gameObject.tag == "Collision") rotate = true;
            }
            else rotate = false;
        }  
    }

    //Shot Behaviour
    float CalculateShotAngle(float Bullet_Speed, Vector3 target) //Calculations are correct
    {
        float distance = Vector3.Distance(Cannon.position, target);

        float parenthesis = Physics.gravity.y * distance * distance; //g * x^2

        double numerator = Math.Sqrt(Math.Pow(Bullet_Speed, 4) - (Physics.gravity.y * parenthesis)); //v^4 - g * (g*x^2)

        double ATangle = ((Math.Pow(Bullet_Speed, 2)) - numerator) / (Physics.gravity.y * distance); //

        double angle = Math.Atan(ATangle);

        float result = (float)angle * Mathf.Rad2Deg;


        print("Angle in Degrees");
        print(result); 
        return result;
    }

    void ShootMissile()
    {
        float X_Angle = CalculateShotAngle(Missile_Speed, Enemy_Target.transform.position);
        
        if(float.IsNaN(Math.Abs(X_Angle)))
        {
            print("Target out of range");
            return;
        }
        
        Turret.transform.Rotate(X_Angle, 0.0f, 0.0f);


        print("Turret ROT" + Turret.transform.eulerAngles);

        Rigidbody missile_inst = Instantiate(Bullet, Cannon.position, Cannon.rotation) as Rigidbody;
        missile_inst.velocity = Missile_Speed * Cannon.forward;

        Shoot_Timer = UnityEngine.Random.Range(2f, 4f);
    }

    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done.
        Current_HP -= amount;

        // Change the UI elements appropriately.
        SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (Current_HP <= 0f && !death)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // Set the flag so that this function is only called once.
        death = true;

        //// Move the instantiated explosion prefab to the tank's position and turn it on.
        //Explosion_Prefab.transform.position = transform.position;
        //Explosion_Prefab.gameObject.SetActive(true);

        //// Play the particle system of the tank exploding.
        //Explosion_Prefab.Play();

        //// Play the tank explosion sound effect.
        //m_ExplosionAudio.Play();

        // Turn the tank off.
        gameObject.SetActive(false);
    }
}
