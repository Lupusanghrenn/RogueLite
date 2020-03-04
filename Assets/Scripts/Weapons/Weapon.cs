using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ScriptableWeapon weapon;

    //Weapon Stats

    private float currentShotsPerSeconds;
    private float fireRate;
    private float fireRateTimer;
    private float currentDamageMultiplier;
    private int currentBulletsPerShot;
    private float currentBulletConeAngle;
    private float currentShotsSpreadAngle;
    private int currentShotsAmmount;

    public GameObject crosshair;
    public Vector2 shotsDirection;
    public List<GameObject> shotsPositions;


    // Bullet Stats

    public GameObject bullet;
    public Bullet currentBullet;

    private float currentBulletSpeed;
    private float currentSpeedDecrease;

    private float currentBulletSize;

    private float currentDamage;
    private float currentFireDamage;
    private float currentPoisonDamage;
    private float currentIceDamage;
    private float currentShockDamage;

    private float currentHorizontalFrequence;
    private float currentVerticalFrequence;
    private float currentHorizontalAmplitude;
    private float currentVerticalAmplitude;

    private float currentExplosionRadius;

    private float currentGravityScale;

    private float currentEnemyPenetration;
    private bool currentWallPenetration;

    private bool currentHoming;

    private float currentTimeBeforeDestruction;


    public float CurrentShotsPerSeconds
    {
        get => currentShotsPerSeconds;
        set
        {
            currentShotsPerSeconds += value;
            if (currentShotsPerSeconds <= 0)
            {
                currentShotsPerSeconds = 1;
            }
            fireRate = 1.0f / currentShotsPerSeconds;
        }
    }

    public float CurrentSpeedDecrease
    {
        get => currentSpeedDecrease;
        set
        {
            currentSpeedDecrease *= value;
            if(currentSpeedDecrease <= 0.95f)
            {
                currentSpeedDecrease = 0.95f;
            }
        }
    }

    public int CurrentBulletsPerShot
    {
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

    public float CurrentBulletConeAngle
    {
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

    public float CurrentShotsSpreadAngle
    {
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

    public int CurrentShotsAmmount
    {
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

    public float CurrentShotsSpeed
    {
        get => currentBulletSpeed;
        set
        {
            currentBulletSpeed += value;
            if (currentBulletSpeed <= 0.5f)
            {
                currentBulletSpeed = 0.5f;
            }
        }
    }

    public float CurrentDamageMultiplier
    {
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

    public float CurrentBulletSize
    {
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

    public float CurrentFireDamage
    {
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

    public float CurrentPoisonDamage
    {
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

    public float CurrentIceDamage
    {
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

    public float CurrentShockDamage
    {
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

    public float CurrentHorizontalFrequence
    {
        get => currentHorizontalFrequence;
        set
        {
            currentHorizontalFrequence += value;
            if (currentHorizontalFrequence <= 0)
            {
                currentHorizontalFrequence = 0;
            }
        }
    }

    public float CurrentVerticalFrequence
    {
        get => currentVerticalFrequence;
        set
        {
            currentVerticalFrequence += value;
            if (currentVerticalFrequence <= 0)
            {
                currentVerticalFrequence = 0;
            }
        }
    }

    public float CurrentHorizontalAmplitude
    {
        get => currentHorizontalAmplitude;
        set
        {
            currentHorizontalAmplitude += value;
            if (currentHorizontalAmplitude <= 0)
            {
                currentHorizontalAmplitude = 0;
            }
        }
    }

    public float CurrentVerticalAmplitude
    {
        get => currentVerticalAmplitude;
        set
        {
            currentVerticalAmplitude += value;
            if (currentVerticalAmplitude <= 0)
            {
                currentVerticalAmplitude = 0;
            }
        }
    }

    public float CurrentExplosionRadius
    {
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

    public float CurrentGravityScale
    {
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

    public float CurrentEnemyPenetration
    {
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

    public bool CurrentWallPenetration
    {
        get => currentWallPenetration;
        set
        {
            currentWallPenetration = value;
        }
    }

    public bool CurrentHoming
    {
        get => currentHoming;
        set
        {
            currentHoming = value;
        }
    }

    public float CurrentTimeBeforeDestruction 
    { 
        get => currentTimeBeforeDestruction;
        set
        {
            currentTimeBeforeDestruction += value;
            if (currentTimeBeforeDestruction < 1.0f)
            {
                currentTimeBeforeDestruction = 1.0f;
            }
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

    void Start()
    {
        currentBullet = bullet.GetComponent<Bullet>();
        UpdateAllWeaponStats();
        weapon.ammo = currentBullet.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        fireRateTimer -= Time.deltaTime;
        if(Input.GetKey(KeyCode.Space) && fireRateTimer <=0)
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
            
        }
    }

    public void Shoot()
    {

        for (int i=0;i< currentShotsAmmount; i++)
        {
            for(int j=0;j< currentBulletsPerShot; j++)
            {
                GameObject bullet = Instantiate(weapon.ammo, shotsPositions[i].transform.position, Quaternion.identity);
                Quaternion.FromToRotation(Vector3.right, shotsDirection);
                bullet.transform.right = shotsDirection;
                float baseDeviation = 0;
                if (currentBulletsPerShot > 1)
                {
                     baseDeviation = j * currentBulletConeAngle / (currentBulletsPerShot - 1) - (currentBulletConeAngle / 2.0f);
                }
                float spreadValue = UnityEngine.Random.Range(-currentShotsSpreadAngle, currentShotsSpreadAngle);
                float rotationAngle = baseDeviation + spreadValue;
                bullet.transform.RotateAround(shotsPositions[i].transform.position, Vector3.forward,rotationAngle);
                UpdateBulletStats(bullet.GetComponent<Bullet>());
            }
        }

        //for ( int i = 0 ; i<currentDroneAmmount ; i++)
        //{
        //    droneList[i].Shoot();
        //}
    }

    private void UpdateBulletStats(Bullet b)
    {
        //Debug.Log(currentBulletSpeed);
        b.Speed = currentBulletSpeed;
        b.SpeedDecrease = CurrentSpeedDecrease;
        b.Size = currentBulletSize;
        b.Damage = currentDamage * currentDamageMultiplier;
        b.FireDamage = currentFireDamage * currentDamageMultiplier;
        b.PoisonDamage = currentPoisonDamage * currentDamageMultiplier;
        b.IceDamage = currentIceDamage * currentDamageMultiplier;
        b.ShockDamage = currentShockDamage * currentDamageMultiplier;
        b.HorizontalFrequence = currentHorizontalFrequence;
        b.VerticalFrequence = currentVerticalFrequence;
        b.HorizontalAmplitude = currentHorizontalAmplitude;
        b.VerticalAmplitude = currentVerticalAmplitude;
        b.ExplosionRadius = currentExplosionRadius;
        b.GravityScale = currentGravityScale;
        b.EnemyPenetration = currentEnemyPenetration;
        b.GoesThroughWalls = currentWallPenetration;
        b.HomingBullet = currentHoming;
        b.TimeBeforeDestruction = currentTimeBeforeDestruction;
    }

    public void UpdateAllWeaponStats()
    {
        currentShotsPerSeconds = weapon.baseShotsPerSeconds;
        fireRate = 1.0f / currentShotsPerSeconds;
        currentBulletsPerShot = weapon.baseBulletsPerShot; 
        currentBulletConeAngle = weapon.baseBulletConeAngle; 
        currentShotsSpreadAngle = weapon.baseShotsSpreadAngle; 
        currentShotsAmmount = weapon.baseShotsAmmount;
        currentDamageMultiplier = weapon.baseDamageMultiplier;

        currentBulletSpeed = currentBullet.baseBullet.baseSpeed;
        currentSpeedDecrease = currentBullet.baseBullet.baseSpeedDecrease;
        currentBulletSize = currentBullet.baseBullet.baseSize;
        currentDamage = currentBullet.baseBullet.baseDamage;
        currentFireDamage = currentBullet.baseBullet.baseFireDamage;      
        currentPoisonDamage = currentBullet.baseBullet.basePoisonDamage;       
        currentIceDamage = currentBullet.baseBullet.baseIceDamage;       
        currentShockDamage = currentBullet.baseBullet.baseShockDamage;
        currentHorizontalFrequence = currentBullet.baseBullet.baseHorizontalFrequence;
        currentVerticalFrequence = currentBullet.baseBullet.baseVerticalFrequence;
        currentHorizontalAmplitude = currentBullet.baseBullet.baseHorizontalAmplitude;
        currentVerticalAmplitude = currentBullet.baseBullet.baseVerticalAmplitude;
        currentExplosionRadius = currentBullet.baseBullet.baseExplosionRadius;       
        currentGravityScale = currentBullet.baseBullet.baseGravityScale;      
        currentEnemyPenetration = currentBullet.baseBullet.baseEnemyPenetration;    
        currentWallPenetration = currentBullet.baseBullet.baseWallPenetration;       
        currentHoming = currentBullet.baseBullet.baseHoming;
        CurrentTimeBeforeDestruction = currentBullet.baseBullet.baseTimeBeforeDestruction;
    }
 }     