using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour
{
    public float projectileSpeed;

    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        //taget från shootern och kommer därför inte kommentera nogrannt,


        Vector3 movementY = Vector3.up * projectileSpeed * Time.deltaTime;


        transform.Translate(movementY);

        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > 12f || transform.position.x < -12f)
        {
            Destroy(gameObject);
        }


    }

    
}
