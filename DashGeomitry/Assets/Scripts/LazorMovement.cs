using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazorMovement : MonoBehaviour
{

    // Use this for initialization

    Vector3 playerPos;

    /// <summary>
    /// hade kunnat använda projectileX scripted för detta men tidigare såg detta script annorlunda ut.
    /// Mycket av det som fanns i BossEyeScriptet fanns här men det funkade inte lika bra. Nu roterar bossens "ögon"
    /// och siktar på spelaren medans tidigare så använde jag Vector3.MoveTowards.
    /// </summary>
 
    float projectileSpeed = 14f;
    

    void Start()
    {
       
        
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.right * projectileSpeed * Time.deltaTime;
        transform.Translate(movement);
        

        if (transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 8f || transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

}
