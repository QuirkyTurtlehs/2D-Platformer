using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleMovement : MonoBehaviour {

	// Use this for initialization

    Vector3 targetPos;
    Vector3 targetPos2;

    

    /// <summary>
    /// Det här scriptet fungerar inte än tanken var att bossen ska sjuta missiler i fas 2 som skapar explosioner
    /// och som i sin tur puttar av spelaren från plattformen. Dock så kolliderar inte den ordentligt men jag orkar inte
    /// fixa det innan jag lämnar in.
    /// </summary>


    public Transform explosion;
    public Transform particles;
	void Start()
    {
        int target1 = BossMouthScript.switchMissileTarget;
        //hittar ett GameObjects position som befinner sig på plattformen och sparar den som en vector.


        if(target1 == 0)
        {
            targetPos = (GameObject.FindGameObjectWithTag("MissileTarget").transform.position);
        }
        else if (target1 == 1)
        {
            targetPos = (GameObject.FindGameObjectWithTag("MissileTarget2").transform.position);
        }

    }
	
	// Update is called once per frame

    void FixedUpdate()
    {
        //förflyttar missilen mot dens mål

        transform.position = Vector3.MoveTowards(transform.position, targetPos, 14 * Time.deltaTime);
         
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        //när missilen kommer fram till målet ska den skapa en explosion.
        if (coll.tag == "MissileTarget")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
        if (coll.tag == "MissileTarget2")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }



}
