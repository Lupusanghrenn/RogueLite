using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Weapon stats

    public float baseShotsPerSeconds;
    private float currentShotsPerSeconds;
    private float fireRate;
    private float fireRateTimer;

    public int baseBulletsPerShot;
    private int currentBulletsPerShot;

    public float baseBulletConeAngle;
    private float currentBulletConeAngle;

    public float baseShotsSpreadAngle;
    private float currentShotsSpreadAngle;

    public int baseShotsAmmount;
    private int currentShotsAmmount;
    public List<GameObject> shotsPositions;

    public Vector2 shotsDirection;

    //Bullet stats

    public GameObject ammo;
    public GameObject crosshair;

    public float baseShotsSpeed;
    private float currentShotsSpeed;

    public float baseDamageMultiplier;
    private float currentDamageMultiplier;

    public float baseBulletSize;
    private float currentBulletSize;

    public float baseFireDamage;
    private float currentFireDamage;

    public float basePoisonDamage;
    private float currentPoisonDamage;

    public float baseIceDamage;
    private float currentIceDamage;

    public float baseShockDamage;
    private float currentShockDamage;

    public float baseHorizontalSpeed; // boomerang
    private float currentHorizontalSpeed;

    public float baseVerticalSpeed; // sinus
    private float currentVerticalSpeed;

    public float baseExplosionRadius;
    private float currentExplosionRadius;

    public float baseGravityScale;
    private float currentGravityScale;

    public float baseEnemyPenetration;
    private float currentEnemyPenetration;

    public bool baseWallPenetration;
    private bool currentWallPenetration;

    public bool baseHoming;
    private bool currentHoming;

    //Drone stats

    public int baseDroneAmmount;
    private int currentDroneAmmount;
    public List<DroneMouvement> droneList;

    public GameObject baseDroneAmmunition;
    private GameObject currentDroneAmmunition;

    public float baseDroneFireRate;
    private float currentDroneFireRate;

    public float baseDronedistance;
    private float currentDronedistance;

    public float baseDroneHorizontalSpeed;
    private float currentDroneHorizontalSpeed;

    public float baseDroneVerticalSpeed;
    private float currentDroneVerticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1.0f / baseShotsPerSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        fireRateTimer -= Time.deltaTime;
        if(Input.GetKey(KeyCode.Space) && fireRateTimer<=0)
        {
            Shoot();
            fireRateTimer = fireRate;
        }

        if(Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0.1f)
        {
            Vector3 crosshairPos = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0);
            crosshairPos.Normalize();
            transform.right = crosshairPos;
            crosshair.transform.position = transform.position + crosshairPos;
            shotsDirection = new Vector2(crosshair.transform.position.x - shotsPositions[0].transform.position.x, crosshair.transform.position.y - shotsPositions[0].transform.position.y);
        }

        Debug.Log(Input.GetAxisRaw("Horizontal"));
        if(shotsDirection.x < 0 )
        {
            //GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<SpriteRenderer>().flipY = true;
            if(transform.rotation.y != 0)
            {
                GetComponent<SpriteRenderer>().flipY = false;

            }
        }
        else
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    public void Shoot()
    {

        for (int i=0;i<currentShotsAmmount;i++)
        {
            for(int j=0;j<currentBulletsPerShot;j++)
            {
                GameObject bullet = Instantiate(ammo, shotsPositions[i].transform.position, Quaternion.identity);
                Quaternion.FromToRotation(Vector3.right, shotsDirection);
                bullet.transform.right = shotsDirection;
               
                float baseDeviation = 0;
                if (currentBulletsPerShot>1)
                {
                     baseDeviation = j * currentBulletConeAngle / (currentBulletsPerShot - 1) - (currentBulletConeAngle / 2.0f);
                }
                float spreadValue = Random.Range(-currentShotsSpreadAngle,currentShotsSpreadAngle);
                float rotationAngle = baseDeviation + spreadValue;
                bullet.transform.RotateAround(shotsPositions[i].transform.position, Vector3.forward,rotationAngle);
                Bullet currentBullet = bullet.GetComponent<Bullet>();
                currentBullet.speed = currentShotsSpeed;
            }
        }

        for ( int i = 0 ; i<currentDroneAmmount ; i++)
        {
            droneList[i].Shoot();
        }
    }
}
