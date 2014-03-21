﻿using UnityEngine;
using System.Collections;

public class TeleportToHubScript : MonoBehaviour 
{

    public GameObject thisObject, player;
    float distance;
	
	// Update is called once per frame
	void Update () 
    {
        distance = Vector2.Distance(player.transform.position, thisObject.transform.position);
        if (Input.GetKeyDown(KeyCode.T) && distance < 2)
        {
            Application.LoadLevel(2);
        }
	}
}