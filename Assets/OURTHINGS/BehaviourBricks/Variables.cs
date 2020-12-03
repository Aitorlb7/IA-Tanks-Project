using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public int Current_Point;
    public float DistancePoint = 5f;
    public int Ammunition;
    public GameObject EnemyGO;
    // Start is called before the first frame update
    void Start()
    {
        Ammunition = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if(Current_Point < 4)
        {
            DistancePoint = Vector3.Distance(gameObject.transform.position, GameObject.Find("Path_Points").transform.GetChild(Current_Point).transform.position);
        }
    }
}
