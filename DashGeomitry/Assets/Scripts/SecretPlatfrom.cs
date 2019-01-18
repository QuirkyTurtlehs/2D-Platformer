using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPlatfrom : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(BossController.bossPhase == 2)
        {
            this.gameObject.SetActive(true);
        }
	}
}
