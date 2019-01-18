using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeScript : MonoBehaviour {

	// Use this for initialization
    public float timer = 0;
    public float rotate = 45;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (timer > 4)
        {
            transform.Rotate(0, 0, rotate);
        }
        else
        {
            timer++;
        }
	}
}
