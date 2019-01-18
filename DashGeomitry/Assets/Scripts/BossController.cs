using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    // Use this for initialization
    bool engagePlayer = false;

    public Animator Anim;
    public float count = 0f;
    public float phase3count = 0f;
    bool immune = false;
    bool smoked = false;
    bool lastBreath = false;
    public static int bossPhase = 0;
    public static float memeCoolDownMax = 5f;
    public static float memeCoolDown = 0f;
    public GameObject SecretPlatform;

    public Transform smokeBomb;
    public Transform Death1;
    public Transform Death2;
    public Transform Death3;
    public Transform Death4;
    public Transform Death5;

    //statiska variabler som används av andra bossens andra delar (separata GameObjects med egna scripts)
    public static bool readyToShoot = false;
    public static bool readyToMissle = false;
    public static bool readyToMeme = false;
    public static int bossHP = 30;

    float deathCount = 0f;
    

    void Start ()
    {
        Anim.GetComponent<Animator>();
        SecretPlatform.SetActive(false);

        //återställer bossen värden när man startar om.
        bossPhase = 0;
        bossHP = 30;
        readyToShoot = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Phase();

        count += Time.deltaTime;
        if (count >= 3)
        {
            Anim.SetBool("Blink", true);
        }
        if (count >= 4)
        {
            Anim.SetBool("Blink", false);
            count = 0f;
        }

    }

    //bossen kommer så småning om ha 3 phases som ändras när spelaren tar bossen till en viss hp.
    void Phase()
    {
        if(bossPhase == 0)
        {
            if (PlayerController.bossEngage == true)
            {
                engagePlayer = true;
            }
            if (engagePlayer == true)
            {
                //Är typ ett intro för bossen, då den långsamt åker ner
                if (transform.position.y > 1f)
                {
                    //bossen förflyttas neråt.
                    Vector3 movement = Vector3.down * 1.2f * Time.deltaTime;
                    transform.Translate(movement);
                }
                else
                {
                    if (bossPhase == 0)
                    {
                        bossPhase = 1;
                    }
                }
            }
            
        }

        if (bossPhase == 1)
        {
            //gör så att bossen kan skjuta laser vilket den endast ska kunna göra i Phase 1.
            readyToShoot = true;

            if (bossHP <= 25)
            {
                Anim.SetBool("isSweating", true);             
                phaseTrans();                
            }
        }
        if (bossPhase == 2)
        {
            readyToMissle = true;
            SecretPlatform.SetActive(true);
             
            if(memeCoolDown < memeCoolDownMax)
            {
                readyToMeme = true;
            }
            else
            {
                memeCoolDown = 0;
            }
            
            if (bossHP <= 15)
            {
                Anim.SetBool("isSweating", true);
                
                if(smoked == false)
                {
                    Instantiate(smokeBomb, transform.position, Quaternion.identity);
                    smoked = true;
                }


                phaseTrans();
            }

        }
        if (bossPhase == 3)
        {
            readyToMissle = true;
            readyToShoot = true;

            if (memeCoolDown < memeCoolDownMax)
            {
                readyToMeme = true;
            }
            else
            {
                memeCoolDown = 0;
            }

            if (bossHP == 10)
            {
                readyToMissle = false;
                readyToShoot = false;
                readyToMeme = false;
                isDead();
            }
        }

    }

    void FixedUpdate()
    {     

    }
    //En transition som görs när bossen byter phase. 
    void phaseTrans()
    {
        //Bossen ska ej kunna sjuta då
        readyToShoot = false;
        readyToMeme = false;
        readyToMissle = false;
        immune = true;
        //Bossen förflyttas bakåt (vänster)
        if (transform.position.x > -5f && bossPhase == 1)
        {   
            Vector3 movement = Vector3.left * 0.8f * Time.deltaTime;
            transform.Translate(movement);
            
        }

        else if ( phase3count <=4 && bossPhase == 2)
        {
            phase3count += Time.deltaTime;

        }

        else
        {
            //Byter phase och gör en trevlig svätt-animation.
            bossPhase++;
            Anim.SetBool("isSweating", false);
            immune = false;
            
                
            
            if(bossPhase == 3)
            {
                readyToMeme = true;
            }
            
        }
        
        
    }

    public void isDead()
    {
        immune = true;

        if (deathCount >= 3)
        {
            Instantiate(Death1, transform.position, Quaternion.identity);
            Instantiate(Death2, transform.position, Quaternion.identity);
            Instantiate(Death3, transform.position, Quaternion.identity);
            Instantiate(Death4, transform.position, Quaternion.identity);
            Instantiate(Death5, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }

        else
        {

            if (lastBreath == true)
            {
                deathCount += Time.deltaTime;
                transform.position = Random.insideUnitCircle * 0.1f;
            }
            if(transform.position.x < 0)
            {
                Vector3 movement = Vector3.right * 1.2f * Time.deltaTime;
                transform.Translate(movement);
            }
            else if (transform.position.x >= 0)
            {
                lastBreath = true;
            }

            
        }
        
        
    }

    

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Om bossen colliderar med spelarens skott så tar bossen skada.
        if (coll.gameObject.tag == "Bullet" && PlayerController.bossEngage == true && immune == false)
        {
            bossHP -= 1;

            //Jag har ej hunnit göra ett sätt att se bossens hp, så detta har jag använt vid testning.
            print(bossHP);   
        }
    }
}
