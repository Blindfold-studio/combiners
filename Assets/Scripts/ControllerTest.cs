using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(Input.GetJoystickNames().Length);
	}
	
	// Update is called once per frame
	void Update () {
        int i = 1;
        while (i <= Input.GetJoystickNames().Length)
        {
            //if (Mathf.Abs(Input.GetAxis("J" + i + "AButton")) > 0.2F || Mathf.Abs(Input.GetAxis("J" + i + "XButton")) > 0.2F)
            Debug.Log("i: " + i + "/" + Input.GetJoystickNames().Length + " name: " + Input.GetJoystickNames()[i-1]);

            i++;
        }
    }
}
