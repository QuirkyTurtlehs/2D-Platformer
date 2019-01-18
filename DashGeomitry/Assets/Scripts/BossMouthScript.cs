using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMouthScript : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    float count = 7f;
    float memeCount = 0f;
    float memeLimit = 0f;
    bool outMemed = false;
    public Transform missilePrefab;
    public Transform mingReePrefab;
    //public Transform haHaaPrefab;
    //public Transform forHeadPrefab;
    public static int switchMissileTarget = 0;



    /// <summary>
    /// Detta script är typ en kopia av BossEye scriptet och har därför ej kommenterat lika mycket.
    /// </summary>

    void Start()
    {
        

        switchMissileTarget = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (memeLimit > 4)
        {
            outMemed = true;
        }
    }
    void FixedUpdate()
    {
        Vector3 TargetPos;

        //hittar spelarens position och sparar den som en vector.
        TargetPos = (GameObject.FindGameObjectWithTag("Player").transform.position);

        //skapar en vecotr som blir hållet "ögat" ska vridas mot.
        Vector3 targetDir = TargetPos - transform.position;

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

        if (BossController.readyToMissle == true)
        {
            if (count >= 10 && BossController.bossPhase == 2 || count >= 10 && BossController.bossPhase == 3)
            {
                Instantiate(missilePrefab, transform.position, Quaternion.AngleAxis(-110, Vector3.forward));
                BossController.readyToShoot = false;              
                count = 0f;

                switchMissileTarget = Random.Range(0, 2);

            }
            else
            {
                count += Time.deltaTime;
            }

        }

        if (BossController.readyToMeme == true)
        {
            if (memeCount >= 0.2 && BossController.bossPhase == 2 && outMemed == false || memeCount >= 0.2 && BossController.bossPhase == 3 && outMemed == false)
            {
                Instantiate(mingReePrefab, transform.position, transform.rotation);
                BossController.readyToMeme = false;
                memeCount = 0f;

                memeLimit ++;
            }
            if (memeLimit > 0 && outMemed == true)
            {
                memeLimit -= Time.fixedDeltaTime * 2;

                if(memeLimit < 1)
                {
                    outMemed = false;
                }
            }
           
            

            else
            {
                memeCount += Time.deltaTime;

            }


        }
    }
}