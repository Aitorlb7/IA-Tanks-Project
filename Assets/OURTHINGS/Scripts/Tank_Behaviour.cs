using System.Collections;
using System.Collections.Generic;
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

    //blue
    public List<GameObject> Path_Points;
    public int Current_Point;
    public UnityEngine.AI.NavMeshAgent Agent;

    //red
    public UnityEngine.AI.NavMeshHit hit;
    public float wanderRadius;
    public RaycastHit hitInfo;
    public LineRenderer line;
    public float lineLenght;

    // Start is called before the first frame update
    void Start()
    {
        Agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Current_HP = HP;
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            Current_HP -= 10;
            print(Current_HP);
        }

        SetHealthUI();
        if(blue_red)
        {
            //BlueRoutine();
        }
        else
        {
            RedRoutine();
        }
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
        float DistancefromRed = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Red").transform.position);

        if(DistancefromRed < 10f)
        {
            GoToTank();
        }
        else
        {
            Patrol();
        }

        Turret.transform.LookAt(GameObject.FindGameObjectWithTag("Red").transform);
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
        Agent.destination = GameObject.FindGameObjectWithTag("Red").transform.position;
    }

    //Red Behaviour
    void RedRoutine()
    {

        Wander();
        
        Turret.transform.LookAt(GameObject.FindGameObjectWithTag("Blue").transform);
    }

    void Wander()
    {
        //float radius = 2f;
        float offset = 3f;
        
        Vector3 localTarget = new Vector3(
            Random.Range(-1.0f, 1.0f), 0,
            Random.Range(-1.0f, 1.0f));
        localTarget.Normalize();
        localTarget *= wanderRadius;
        localTarget += new Vector3(0, 0, offset);


        Vector3 worldTarget =
            transform.TransformPoint(localTarget);
        worldTarget.y = 0f;

        LineRaycast();
        Agent.SetDestination(worldTarget);

    }
    void LineRaycast()
    {
        if(line.enabled)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * lineLenght);
            if(Physics.Raycast(transform.position,transform.forward,out hitInfo, lineLenght))
            {
                if(hitInfo.collider.gameObject.tag == "Collision")
                {
                    if (Agent.isStopped == false) print(hitInfo.collider.gameObject.name);
                    Agent.isStopped = true;
                    
                }

            }
        }

        
    }


    //void Seek(Vector3 target)
    //{
    //    Agent.destination = target;
    //}
}
