﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int health = 100;
    static public GameManager instance;

    public Light[] lights;

    public Color globalLightColor;

    public Transform obstaclesParent;

    public List<DealDamage> obstacles = new List<DealDamage>();

    public RaycastHandler raycastHandler;

    bool lightsOn = true;




    void Awake()
    {
        instance = this;

        lights = FindObjectsOfType<Light>();

    }




    void Start()
    {
        foreach (Transform obj in obstaclesParent)
        {
            if(obj.GetComponent<DealDamage>())
            {
                obstacles.Add(obj.GetComponent<DealDamage>());
            }
        }
    }



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            raycastHandler.Interact();
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            lightsOn = !lightsOn;
            if (lightsOn)
            {
                TurnLightsOn();
            }
            else
            {
                TurnLightsDown();
            }

        }
    }




    public void TakeDamage(int amount)
    {
        this.health -= amount;
        Debug.Log(health);

        if (health <= 0)
        {
            LoseGame();
        }
    }




    public void LoseGame()
    {
        //???
    }


    public void StartGame()
    {

    }


    public void TurnLightsDown()
    {
        foreach(Light mlight in lights)
        {
            mlight.enabled = false;
        }
        RenderSettings.ambientLight = Color.black;

        foreach(DealDamage obs in obstacles)
        {
            obs.HideObj();
        }
    }


    public void TurnLightsOn()
    {
        foreach (Light mlight in lights)
        {
            mlight.enabled = true;
        }
        RenderSettings.ambientLight = globalLightColor;

        foreach (DealDamage obs in obstacles)
        {
            obs.ShowObj();
        }
    }
}
