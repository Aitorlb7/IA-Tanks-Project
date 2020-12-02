using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public List<GameObject> Path_Points;

    // Start is called before the first frame update
    void Start()
    {
        if (Path_Points.Count < GameObject.Find("Path_Points").transform.childCount)
        {
            for (int i = 0; i < GameObject.Find("Path_Points").transform.childCount; i++)
            {
                Path_Points.Add(GameObject.Find("Path_Points").transform.GetChild(i).gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
