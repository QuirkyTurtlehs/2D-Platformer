using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatformContr : MonoBehaviour {

    // Use this for initialization

    

	void awake()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Script som förstör platformen när bossfighten startas.
		if (PlayerController.bossEngage == true)
        {
            Destroy(this.gameObject);
            

        }
	}
}
