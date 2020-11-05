using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Game_man : MonoBehaviour
{
    //UI
    public Text Message;

    //----------
    public struct Timer
    {
        public float time;
        public bool enabled;
    }

    public GameObject BlueTank_prefab, RedTank_prefab;
    private int num_Rounds = 0;
    public List<GameObject> Red_Spawnpoints, Blue_Spawnpoints, Current_Tanks;
    public Timer Start_Time, Round_Time;
    public Camera_behaviour Camera;

    //TANK 0 = BLUE, TANK 1 = RED

    // Start is called before the first frame update
    void Start()
    {
        Start_Time.time = 3;
        Start_Time.enabled = true;

        SetSpawnPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if(Start_Time.enabled == true)
        {
            Message.text = "" + (int)Start_Time.time;
            Start_Time.time -= Time.deltaTime;
        }

        if(Start_Time.time <= 0)
        {
            Current_Tanks[0] = Instantiate(BlueTank_prefab, Blue_Spawnpoints[num_Rounds].transform.position, new Quaternion(0, 0, 0, 0));
            Current_Tanks[1] = Instantiate(RedTank_prefab, Red_Spawnpoints[num_Rounds].transform.position, new Quaternion(0, 0, 0, 0));
            SetCameraTargets();
            Camera.SetStartPositionAndSize();
            num_Rounds++;
            Message.text = string.Empty;
            Start_Time.time = 3;
            Start_Time.enabled = false;
        }

        if(Start_Time.enabled == false) //Round
        {
            
        }
    }


    private void SetSpawnPoints()
    {
        for (int i = 0; i < GameObject.Find("Red_Points").transform.childCount; i++)
        {
            Red_Spawnpoints.Add(GameObject.Find("Red_Points").transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < GameObject.Find("Blue_Points").transform.childCount; i++)
        {
            Blue_Spawnpoints.Add(GameObject.Find("Blue_Points").transform.GetChild(i).gameObject);
        }
    }

    private void SetCameraTargets()
    {
        Camera.Tanks[0] = Current_Tanks[0];
        Camera.Tanks[1] = Current_Tanks[1];
    }
}
