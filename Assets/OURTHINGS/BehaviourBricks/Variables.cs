using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Variables : MonoBehaviour
{
    [HideInInspector] public float ShootTimer;
    [HideInInspector] public int Current_Point;
    [HideInInspector] public float DistancePoint = 5f;
    [HideInInspector] public int Ammunition = 5;
    public GameObject EnemyGO;
    public GameObject Base;
    [HideInInspector] public bool IsEmpty = false;
    public GameObject Explosion_Prefab;

    //LifeSystem
    [HideInInspector] public float HP = 100f;
    public float Current_HP;
    public Image FillImage;
    public Slider SliderHealth;
    public Color FullHealth_Color;
    public Color ZeroHealth_Color = Color.black;
    [HideInInspector] public bool isDead;

    //Rounds
    [HideInInspector] public int Win_Count = 0;

    public List<GameObject> Ammo_Images;

    void Start()
    {
        ShootTimer = UnityEngine.Random.Range(2f, 4f);
        isDead = false;
        Current_HP = HP;
        Ammunition = 5;
        IsEmpty = false;

        for (int i = 0; i < 5; i++)
        {
            if(gameObject.tag == "Red")
                Ammo_Images.Add(GameObject.Find("MessageCanvas").transform.Find("RedMuni").transform.GetChild(i).transform.gameObject);

            if (gameObject.tag == "Blue")
                Ammo_Images.Add(GameObject.Find("MessageCanvas").transform.Find("BlueMuni").transform.GetChild(i).transform.gameObject);
        }

        if (gameObject.tag == "Red")
            Base = GameObject.Find("RedBase").gameObject;

        if (gameObject.tag == "Blue")
            Base = GameObject.Find("BlueBase").gameObject;
    }

    void Update()
    {
        ShootTimer -= Time.deltaTime;
        if (Current_Point < 4)
        {
            DistancePoint = Vector3.Distance(gameObject.transform.position, GameObject.Find("Path_Points").transform.GetChild(Current_Point).transform.position);
        }

        if(Ammunition <= 0)
        {
            Ammunition = 0;
            IsEmpty = true;
        }

        SetHealthUI();
    }

    private void SetHealthUI()
    {
        SliderHealth.value = Current_HP;
        FillImage.color = Color.Lerp(ZeroHealth_Color, FullHealth_Color, Current_HP / HP);

        if (Current_HP <= 0)
        {
            OnDeath();
        }
    }

    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done.
        Current_HP -= amount;

        // Change the UI elements appropriately.
        SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (Current_HP <= 0f && !isDead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // Set the flag so that this function is only called once.
        isDead = true;

        //// Move the instantiated explosion prefab to the tank's position and turn it on.
        Explosion_Prefab.transform.position = transform.position;
        Explosion_Prefab.gameObject.SetActive(true);

        //// Play the particle system of the tank exploding.
        //Explosion_Prefab.Play();

        //// Play the tank explosion sound effect.
        //m_ExplosionAudio.Play();

        // Turn the tank off.
        gameObject.SetActive(false);
    }
}
