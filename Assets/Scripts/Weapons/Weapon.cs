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

    public float CurrentShotsPerSeconds {
        get => currentShotsPerSeconds;
        set {
            currentShotsPerSeconds += value;
            if(currentShotsPerSeconds<=0)
            {
                currentShotsPerSeconds = 1;
            }
            fireRate = 1.0f / currentShotsPerSeconds;
        }
    }

    public int CurrentBulletsPerShot {
        get => currentBulletsPerShot;
        set
        {
            currentBulletsPerShot += value;
            if (currentBulletsPerShot <= 0)
            {
                currentBulletsPerShot = 1;
            }
        }
    }

    public float CurrentBulletConeAngle {
        get => currentBulletConeAngle;
        set
        {
            currentBulletConeAngle += value;
            if (currentBulletConeAngle <= 0)
            {
                currentBulletConeAngle = 0;
            }
        }
    }

    public float CurrentShotsSpreadAngle {
        get => currentShotsSpreadAngle;
        set
        {
            currentShotsSpreadAngle += value;
            if (currentShotsSpreadAngle <= 0)
            {
                currentShotsSpreadAngle = 0;
            }
        }
    }

    public int CurrentShotsAmmount {
        get => currentShotsAmmount;
        set
        {
            currentShotsAmmount += value;
            if (currentShotsAmmount <= 1)
            {
                currentShotsAmmount = 1;
            }
        }
    }

    public float CurrentShotsSpeed {
        get => currentShotsSpeed;
        set
        {
            currentShotsSpeed += value;
            if (currentShotsSpeed <= 0.5f)
            {
                currentShotsSpeed = 0.5f;
            }
        }
    }

    public float CurrentDamageMultiplier {
        get => currentDamageMultiplier;
        set
        {
            currentDamageMultiplier += value;
            if (currentDamageMultiplier <= 0.1f)
            {
                currentDamageMultiplier = 0.1f;
            }
        }
    }

    public float CurrentBulletSize {
        get => currentBulletSize;
        set
        {
            currentBulletSize += value;
            if (currentBulletSize <= 0.1f)
            {
                currentBulletSize = 0.1f;
            }
        }
    }

    public float CurrentFireDamage {
        get => currentFireDamage;
        set
        {
            currentFireDamage += value;
            if (currentFireDamage <= 0)
            {
                currentFireDamage = 0;
            }
        }
    }

    public float CurrentPoisonDamage {
        get => currentPoisonDamage;
        set
        {
            currentPoisonDamage += value;
            if (currentPoisonDamage <= 0)
            {
                currentPoisonDamage = 0;
            }
        }
    }

    public float CurrentIceDamage {
        get => currentIceDamage;
        set
        {
            currentIceDamage += value;
            if (currentIceDamage <= 0)
            {
                currentIceDamage = 0;
            }
        }
    }

    public float CurrentShockDamage {
        get => currentShockDamage;
        set
        {
            currentShockDamage += value;
            if (currentShockDamage <= 0)
            {
                currentShockDamage = 0;
            }
        }
    }

    public float CurrentHorizontalSpeed {
        get => currentHorizontalSpeed;
        set
        {
            currentHorizontalSpeed += value;
            if (currentHorizontalSpeed <= 0)
            {
                currentHorizontalSpeed = 0;
            }
        }
    }

    public float CurrentVerticalSpeed {
        get => currentVerticalSpeed;
        set
        {
            currentVerticalSpeed += value;
            if (currentVerticalSpeed <= 0)
            {
                currentVerticalSpeed = 0;
            }
        }
    }

    public float CurrentExplosionRadius {
        get => currentExplosionRadius;
        set
        {
            currentExplosionRadius += value;
            if (currentExplosionRadius <= 0)
            {
                currentExplosionRadius = 0;
            }
        }
    }

    public float CurrentGravityScale {
        get => currentGravityScale;
        set
        {
            currentGravityScale += value;
            if (currentGravityScale <= 0)
            {
                currentGravityScale = 0;
            }
        }
    }

    public float CurrentEnemyPenetration {
        get => currentEnemyPenetration;
        set
        {
            currentEnemyPenetration += value;
            if (currentEnemyPenetration <= 0)
            {
                currentEnemyPenetration = 0;
            }
        }
    }

    public bool CurrentWallPenetration {
        get => currentWallPenetration;
        set
        {
            currentWallPenetration = value;
        }
    }

    public bool CurrentHoming {
        get => currentHoming;
        set
        {
            currentHoming = value;
        }
    }

    //Drone stats

    //public int baseDroneAmmount;
    //private int currentDroneAmmount;
    //public List<DroneMouvement> droneList;

    //public GameObject baseDroneAmmunition;
    //private GameObject currentDroneAmmunition;

    //public float baseDroneFireRate;
    //private float currentDroneFireRate;

    //public float baseDronedistance;
    //private float currentDronedistance;

    //public float baseDroneHorizontalSpeed;
    //private float currentDroneHorizontalSpeed;

    //public float baseDroneVerticalSpeed;
    //private float currentDroneVerticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAllWeaponStats();
        
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

        if(Input.GetKeyDown(KeyCode.A))
        {
            CurrentBulletConeAngle = -10;
            Debug.Log(CurrentBulletConeAngle);
        }
    }

    public void Shoot()
    {

        for (int i=0;i<CurrentShotsAmmount;i++)
        {
            for(int j=0;j<CurrentBulletsPerShot;j++)
            {
                GameObject bullet = Instantiate(ammo, shotsPositions[i].transform.position, Quaternion.identity);
                Quaternion.FromToRotation(Vector3.right, shotsDirection);
                bullet.transform.right = shotsDirection;
               
                float baseDeviation = 0;
                if (CurrentBulletsPerShot>1)
                {
                     baseDeviation = j * CurrentBulletConeAngle / (CurrentBulletsPerShot - 1) - (CurrentBulletConeAngle / 2.0f);
                }
                float spreadValue = Random.Range(-CurrentShotsSpreadAngle,CurrentShotsSpreadAngle);
                float rotationAngle = baseDeviation + spreadValue;
                bullet.transform.RotateAround(shotsPositions[i].transform.position, Vector3.forward,rotationAngle);
                Bullet currentBullet = bullet.GetComponent<Bullet>();
                currentBullet.speed = CurrentShotsSpeed;
            }
        }

        //for ( int i = 0 ; i<currentDroneAmmount ; i++)
        //{
        //    droneList[i].Shoot();
        //}
    }

    public void UpdateAllWeaponStats()
    {
       currentShotsPerSeconds = baseShotsPerSeconds;
       fireRate = 1.0f / currentShotsPerSeconds;
       CurrentBulletsPerShot =baseBulletsPerShot; 
       CurrentBulletConeAngle = baseBulletConeAngle; 
       CurrentShotsSpreadAngle=baseShotsSpreadAngle; 
       CurrentShotsAmmount=baseShotsAmmount;       
       CurrentShotsSpeed=baseShotsSpeed;      
       CurrentDamageMultiplier=baseDamageMultiplier;       
       CurrentBulletSize=baseBulletSize;       
       CurrentFireDamage=baseFireDamage;      
       CurrentPoisonDamage=basePoisonDamage;       
       CurrentIceDamage=baseIceDamage;       
       CurrentShockDamage=baseShockDamage;       
       CurrentHorizontalSpeed=baseHorizontalSpeed;
       CurrentVerticalSpeed=baseVerticalSpeed;      
       CurrentExplosionRadius=baseExplosionRadius;       
       CurrentGravityScale=baseGravityScale;      
       CurrentEnemyPenetration=baseEnemyPenetration;    
       CurrentWallPenetration=baseWallPenetration;       
       CurrentHoming = baseHoming;     
    }
 }     