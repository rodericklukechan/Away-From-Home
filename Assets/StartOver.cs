﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown((KeyCode.Space))){

            //reset the scene
            Application.LoadLevel("NancyMain2");

        }
		
	}
}
