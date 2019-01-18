using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    // Use this for initialization
    public float projectileSpeed;

    public bool shootRight = true;

    // Update is called once per frame
    void Awake()
    {
        shootRight = PlayerController.isFlipped;
    }

    void Update ()
    {
        

        if (shootRight == true)
        {
            Vector3 movementX = Vector3.left * projectileSpeed * Time.deltaTime;
            transform.Translate(movementX);
        }
        else if (shootRight == false)
        {
            Vector3 movementX = Vector3.right * projectileSpeed * Time.deltaTime;
            transform.Translate(movementX);
        }

        if (transform.position.x > 12f)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -12f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Boss")
        {
            Destroy(this.gameObject);

        }
    }

}
