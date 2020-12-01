using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Game_man : MonoBehaviour
{
    //UI
    public Text Message;
    public Text Rounds_num_Text;
    public Button NextRound_Button;
    public Button Restart_Button;
    public Text Victory_count;
    public Text BlueMuni_text, RedMuni_text;
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
        Rounds_num_Text.text = ("Round: " + num_Rounds);

        if(Start_Time.enabled == true) //Start Timer
        {
            Message.text = "" + (int)Start_Time.time;
            Start_Time.time -= Time.deltaTime;
        }

        if(Start_Time.time <= 0) //Create Tanks
        {
            Current_Tanks[0] = Instantiate(BlueTank_prefab, Blue_Spawnpoints[num_Rounds].transform.position, new Quaternion(0, 0, 0, 0));
            Current_Tanks[1] = Instantiate(RedTank_prefab, Red_Spawnpoints[num_Rounds].transform.position, new Quaternion(0, 0, 0, 0));
            SetCameraTargets();
            Camera.SetStartPositionAndSize();
            num_Rounds++;
            Message.text = string.Empty;
            Start_Time.time = 3;
            Victory_count.text = "RED " + Current_Tanks[1].GetComponent<Tank_Behaviour>().Win_Count + " - " + Current_Tanks[0].GetComponent<Tank_Behaviour>().Win_Count + " BLUE";
            Start_Time.enabled = false;
        }

        if(Start_Time.enabled == false) //Rounds
        {
            if(Current_Tanks[0].GetComponent<Tank_Behaviour>().Current_num_bullets <= 0) // When blue recharge
            {
                BlueMuni_text.text = "" + (int)Current_Tanks[0].GetComponent<Tank_Behaviour>().Recharge_Timer;
            }
            else
            {
                 BlueMuni_text.text = "" + Current_Tanks[0].GetComponent<Tank_Behaviour>().Current_num_bullets;
            } 
            
            if(Current_Tanks[0].GetComponent<Tank_Behaviour>().Current_num_bullets <= 0) // When blue recharge
            {
                RedMuni_text.text = "" + (int)Current_Tanks[1].GetComponent<Tank_Behaviour>().Recharge_Timer;
            }
            else  
            {
                RedMuni_text.text = "" + Current_Tanks[1].GetComponent<Tank_Behaviour>().Current_num_bullets;
            }       

            if(Current_Tanks[0].GetComponent<Tank_Behaviour>().isDead == true) // When Blue dies
            {
                Current_Tanks[1].GetComponent<Tank_Behaviour>().Win_Count++;
                Victory_count.text = "RED " + Current_Tanks[1].GetComponent<Tank_Behaviour>().Win_Count + " - " + Current_Tanks[0].GetComponent<Tank_Behaviour>().Win_Count + " BLUE";
                Message.text = "Red Wins";
                Current_Tanks[1].SetActive(false);
                NextRound_Button.gameObject.SetActive(true);
                Current_Tanks[0].GetComponent<Tank_Behaviour>().isDead = false;
            }
            if (Current_Tanks[1].GetComponent<Tank_Behaviour>().isDead == true) // When Red dies
            {
                Current_Tanks[0].GetComponent<Tank_Behaviour>().Win_Count++;
                Victory_count.text = "RED " + Current_Tanks[1].GetComponent<Tank_Behaviour>().Win_Count + " - " + Current_Tanks[0].GetComponent<Tank_Behaviour>().Win_Count + " BLUE";
                Message.text = "Blue Wins";
                Current_Tanks[0].SetActive(false);
                NextRound_Button.gameObject.SetActive(true);
                Current_Tanks[1].GetComponent<Tank_Behaviour>().isDead = false;
            }

            for(int i = 0; i < 2; i++)
            {
                if(Current_Tanks[i].GetComponent<Tank_Behaviour>().Win_Count >= 3)
                {
                    if (Current_Tanks[i].GetComponent<Tank_Behaviour>().isBlue)
                        Message.text = "Blue wins the game";
                    else
                        Message.text = "Red wins the game";

                    NextRound_Button.gameObject.SetActive(false);
                    Restart_Button.gameObject.SetActive(true);
                }
            }
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

    public void RestartGame()
    {
        for (int i = 0; i < 2; i++)
        {
            Object.Destroy(Current_Tanks[i]);
            num_Rounds = 0;
            Start_Time.enabled = true;
        }
        Restart_Button.gameObject.SetActive(false);
    }
    public void GoToNextRound()
    {
        num_Rounds++;

        Current_Tanks[0].GetComponent<Tank_Behaviour>().Current_HP = Current_Tanks[0].GetComponent<Tank_Behaviour>().HP;
        Current_Tanks[1].GetComponent<Tank_Behaviour>().Current_HP = Current_Tanks[1].GetComponent<Tank_Behaviour>().HP;
        Current_Tanks[0].transform.position = Blue_Spawnpoints[1].transform.position;
        Current_Tanks[1].transform.position = Red_Spawnpoints[1].transform.position;
        Current_Tanks[0].GetComponent<Tank_Behaviour>().isDead = false;
        Current_Tanks[1].GetComponent<Tank_Behaviour>().isDead = false;
        Current_Tanks[0].SetActive(true);
        Current_Tanks[1].SetActive(true);

        Message.text = string.Empty;

        NextRound_Button.gameObject.SetActive(false);
    }
}
