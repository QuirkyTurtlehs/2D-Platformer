using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool leftForce = false;
    public static bool conCollision = false;

    [SerializeField]
    bool godMode = false;   

    public Rigidbody2D rb;

    //triggers
    public static bool saw = false;
    public static bool bossEngage = false;

    //
    public SpriteRenderer sr;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public static bool isFlipped = false;
    bool hasSawTriggered = false;

    //jumping
    public float force = 300f;
    public int dubbleJump = 0;

    public bool onGround = false;

    //shooting
    public float coolDown = 0;
    public float coolDownLimit = 0.2f;
    public GameObject bulletPrefab;

    //movement
    public float speed = 300f;
    public float velocityX;
    float Stamina = 0;
    float slowAmount = 0;
    float boost = 1F;
    bool hitByExplosion = false;



    public bool canNotMove = false;
    bool isSlowed = false;

    

    //animator
    public Animator Anim;
    //material
    public PhysicsMaterial2D forceLeft;
    public PhysicsMaterial2D forceRight;

    //Text
    public Text timeText;
    public Text deadText;

    //Level
    public bool levelComplete = false;
    public static float gameTime = 0;
    public float levelTime = 0f;

    [SerializeField]




    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        Anim.GetComponent<Animator>();
        sr.GetComponent<SpriteRenderer>();

        bossEngage = false;
        rb.mass = 1;
    }

    // Update is called once per frame
    void Update()
    {
        leftForce = false;

        //kollar om spelaren är "på marken" och sparar det i onGround. taget från: http://answers.unity3d.com/questions/610960/2d-check-if-player-is-on-the-ground.html
        onGround = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);

        if (conCollision == true)
        {
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        if (conCollision == false)
        {
            rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }

        //Baserat av unity3d dokumentation: https://docs.unity3d.com/ScriptReference/Rigidbody.AddForce.html


        //tillåter spelaren att hoppa om spelaren är på marken eller en gång i luften om spelaren inte har hoppat i luften tidigare 
        if (Input.GetButtonDown("Zjump") && onGround == true || Input.GetButtonDown("Zjump") && onGround == false && dubbleJump < 1)
        {
            rb.velocity = new Vector3(velocityX, 0);
            //Ökar spelarens velocityX efter ett hopp.
            boost += 0.15F;

            rb.AddForce(new Vector3(rb.velocity.x, force - Mathf.Pow(Stamina, 4), 0));
            //onGroun        
            dubbleJump++;

        }
        

        if (Input.GetButtonDown("xShoot") && coolDown > coolDownLimit)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            coolDown = 0;
        }
        if (coolDown <= coolDownLimit)
        {
            coolDown += Time.deltaTime;
        }
        
        //Gör dubbel hopp möjligt om spelaren inte precis har hoppat. 
        if (onGround == true)
        {
            dubbleJump = 0;

            //tar bort spelaren "boost"
            boost = 1;
        }


        //körs bl.a när spelaren är död
        if (canNotMove == true)
        {
            speed = 0;
            force = 0;
        }
        //gör spelaren långsamn i ett tids intervall (stamina påverkar också spelarens förmåga att hoppa)
        if (isSlowed == true && slowAmount < 1)
        {
            Stamina = 3;
            slowAmount += Time.deltaTime;
        }
        //Om spelaren varit långsam i 0.9 sec så är inte spelaren längre långsam.
        if (slowAmount > 0.9)
        {
            isSlowed = false;
            slowAmount = 0;
            Stamina = 0;
        }

        if (levelComplete != true)
        {
            levelTime += Time.deltaTime;


            timeText.text = "Time: " + string.Format("{0:0.00}", levelTime);
        }
        
        

    }

    void FixedUpdate()
    {



        //sparar spelarens blivande horizontal velocity  
        velocityX = Input.GetAxisRaw("Horizontal") * speed * boost * Time.deltaTime;

        //om stamina är mer än 0 så blir spelarens velocity mindre 
        if (velocityX < 0)
        {
            //om spelaren går vänster
            velocityX += Stamina;
            isFlipped = true;
            sr.flipX = true;
        }
        if (velocityX > 0)
        {
            //om spelaren går höger
            velocityX -= Stamina;
            isFlipped = false;
            sr.flipX = false;
        }

        //ändrar spelarens velocity
        rb.velocity = new Vector3(velocityX, rb.velocity.y);

        if (hitByExplosion == true)
        {
            rb.velocity = new Vector3(-10, rb.velocity.y);
        }


        //vänder Spriten om spelaren går åt vänster
        


    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Spike")
        {
            IsDead();
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {


        if (coll.gameObject.tag == "SawTrigger" && hasSawTriggered == false)
        {
            saw = true;
            hasSawTriggered = true;
        }
        if (coll.gameObject.tag == "Boss Engage")
        {
            bossEngage = true;
        }
        if (coll.gameObject.tag == "Lethal")
        {
            IsDead();
        }

        if (coll.gameObject.tag == "finishline")
        {
            gameTime += levelTime;

            levelComplete = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            
        }


        if (coll.gameObject.tag == "Stone")
        {
            //gör spelaren långsam, används inte i spelet än.
            isSlowed = true;
        }

        //tillåter Baby-huvuden att collidera med spelaren
        if (coll.gameObject.tag == "Baby")
        {

            //spelaren står i den vänstra triggern
            if (coll.sharedMaterial == forceLeft)
            {
                leftForce = true;
                //knuffar spelaren åt vänster
                rb.AddForce(new Vector3(-force * 7, force * 0.5f, 0));

                //detta körs om spelaren står i båda.
            }
            //omspelaren står i den högra triggern och inte står i den vänstra
            if (coll.sharedMaterial == forceRight && leftForce == false)
            {
                //knuffar spelaren åt höger
                rb.AddForce(new Vector3(force * 7, force * 0.5f, 0));

                //detta körs INTE om spelaren står i båda.
            }

        }
        if (coll.gameObject.tag == "Explosion" && godMode == false)
        {
            
            rb.AddForce(new Vector3(0, force * 2, 0));
            hitByExplosion = true;            


        }

    }

    //om spelaren är död
    public void IsDead()
    {
        if(godMode == false)
        {
            canNotMove = true;
            Anim.SetBool("isDead", true);
            deadText.text = "You dead. Press 'R' to restart.";

        }    

    }
    

}
