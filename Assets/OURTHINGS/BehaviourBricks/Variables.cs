using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public int Current_Point;
    public float DistancePoint = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DistancePoint = Vector3.Distance(gameObject.transform.position, GameObject.Find("Path_Points").transform.GetChild(Current_Point).transform.position);
    }
}
