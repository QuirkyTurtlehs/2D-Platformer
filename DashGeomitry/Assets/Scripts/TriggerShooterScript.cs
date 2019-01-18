using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShooterScript : MonoBehaviour {

    // Use this for initialization
    public GameObject sawPrefab;
    bool hasShot = false;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Script som för en fälla

        //om spelaren har triggat fällan och fällan inte redan blivit aktiverad.
        if (PlayerController.saw == true && hasShot == false)
        {
            Instantiate(sawPrefab, transform.position, Quaternion.identity);
            hasShot = true;
            PlayerController.saw = false;
        }

    }
}
