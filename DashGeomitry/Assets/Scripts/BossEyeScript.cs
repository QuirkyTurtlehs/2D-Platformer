using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeScript: MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    GameObject lazerPrefab;


    [SerializeField]
    
    float count;



	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
    void FixedUpdate()
    {

        Vector3 playerPos;
              
        //hittar spelarens position och sparar den som en vector.
        playerPos = (GameObject.FindGameObjectWithTag("Player").transform.position);

        //skapar en vecotr som blir hållet "ögat" ska vridas mot.
        Vector3 targetDir =  playerPos - transform.position;

        //Massa matte som jag fick hjälp av Vilde med. Räknar ut vinkeln som "ögat" ska vrida sig till antar jag.
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg);

        //skapar en quaternion med vikeln angel som sedan ska användas med Lerp.
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        //utför rotationen
        transform.rotation = Quaternion.Lerp(transform.rotation, q, 10);

        Shoot();
    }

    void Shoot()
    {
      //om bossen är kan sjuta, alltså i phase 1 så skapar den en lazer.
        if (BossController.readyToShoot == true)
        {
            if (count >= 6 && BossController.bossPhase == 1 || count >= 6 && BossController.bossPhase == 3)
            {
                //skapar laser
                Instantiate(lazerPrefab, transform.position, transform.rotation);
                BossController.readyToShoot = false;
                count = 0f;
            }
            else
            {
                //jag tyckte att Bossen blev alldeles för förutsigbar och la därför till lite RNG.
                //Nu finns det chans att bossen sjuter tidigare än vanligt och att de två laser projektilerna
                //blir osynkade. Tidigare kunde man hela tiden lura bossen att sjuta högt genom att hoppa precis 
                //innan den sköt vilket nu kan ha förödande konsekvenser då bossen kan sjuta tidigare än vanligt och
                //sjuta medans spelaren hoppa.

                //ända jobbiga är att om man få tillräckligt dålig RNG så blir bossen omöjlig, då skotten kan vara så pass
                //osynkade att det inte går att hoppa över dom. Men men livet är inte alltid rättvist.
                 
                count += Time.deltaTime + Random.Range(0, 0.08f);
            }
            
        }
    }

}
   