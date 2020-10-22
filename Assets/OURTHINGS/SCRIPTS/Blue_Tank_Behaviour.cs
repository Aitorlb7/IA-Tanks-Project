using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class Blue_Tank_Behaviour : MonoBehaviour
    {
        public List<GameObject> Path_Points;
        private int Current_Point;

        // Start is called before the first frame update
        void Start()
        {         
            for (int i = 0; i < GameObject.Find("Path_Points").transform.childCount; i++)
            {
                Path_Points.Add(GameObject.Find("Path_Points").transform.GetChild(i).gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            float DistancePoint = Vector3.Distance(transform.position, Path_Points[Current_Point].transform.position);
            bool Direction = true;

            if (Direction)
            {
                if (DistancePoint < 4)
                {
                    Current_Point++;

                    if (Current_Point >= Path_Points.Count)
                    {
                        Current_Point--;

                        Direction = false;
                    }
                }
                else
                {
                    Current_Point--;

                    if (Current_Point < 0)
                    {
                        Current_Point++;
                        Direction = true;
                    }
                }
            }


        }

    }
}
