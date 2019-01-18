using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManagement : MonoBehaviour {


    public Toggle collionToggle;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //tillåter spelaren att starta om scenen vid t.ex. död. då "r" trycks
        if (Input.GetButtonDown("Respawn"))
        {

            //"ändrar" scenen till den nuvarande scenen, spelaren börjar alltså om.
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);

        }


    }

    public void ContinuousColliosion (bool collisionValue)
    {
        PlayerController.conCollision = collisionValue;
    }
}
