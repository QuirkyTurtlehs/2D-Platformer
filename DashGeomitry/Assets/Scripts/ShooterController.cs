using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour {

    // Use this for initialization
    public float count = 0;
    public GameObject bulletPrefab;
    public float limit = 1.5f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (count > limit)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
           

            count = 0;
        }
        else
        {
            count += Time.deltaTime;

        }

        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
