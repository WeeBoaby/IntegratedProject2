﻿using UnityEngine;
using System.Collections;

public class TeleportToLevel1 : MonoBehaviour
{

    public GameObject player;
    float distance;

	
	// Update is called once per frame
	void Update () 
    {
        distance = Vector2.Distance(player.transform.position, this.transform.position);
        if (Input.GetKeyDown(KeyCode.E) && distance < 2)
        {
            Application.LoadLevel(1);
        }
	}
}
