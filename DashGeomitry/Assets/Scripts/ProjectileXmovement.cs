using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileXmovement : MonoBehaviour
{

    // Use this for initialization
    public float projectileSpeed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        /*Scripts för projektiler i x-led. Hade kunnat gjort detta och y-led i ett script men kände mig lat. Om det hade varit större
        skillnader mellan scripsen hade jag föredragit dem separata så här.*/

        Vector3 movementX = Vector3.left * projectileSpeed * Time.deltaTime;

        transform.Translate(movementX);
        

        if (transform.position.x > 12f)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 6f || transform.position.y < -6f)
        {
            Destroy(gameObject);
        }

    }

    //Används ej då spelaren inte kan sjuta (än) och att det inte finns fiender (än).
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);

        }

        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);

        }

    }
}
