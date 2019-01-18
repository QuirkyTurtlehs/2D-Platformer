using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTargetMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		float playerPosX = (GameObject.FindGameObjectWithTag("Player").transform.position.x);
        float playerPosY = (GameObject.FindGameObjectWithTag("Player").transform.position.y);



        transform.position = new Vector3(playerPosX + 1, playerPosY -1  );
    }
}
