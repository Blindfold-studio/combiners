﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int i = 1;
        while (i <= 2)
        {
            if (Mathf.Abs(Input.GetAxis("J" + i + "AButton")) > 0.2F || Mathf.Abs(Input.GetAxis("J" + i + "XButton")) > 0.2F)
                Debug.Log(Input.GetJoystickNames()[i-1] + " is moved");

            i++;
        }
    }
}
