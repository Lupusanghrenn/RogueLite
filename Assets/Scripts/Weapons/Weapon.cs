using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shotsPerSeconds;
    private float fireRate;
    private float fireRateTimer;
    public float shotsSpeed;
    public int bulletsPerShot;
    public float bulletConeAngle;
    public float shotsSpreadAngle;
    public int shotsAmmount;
    public List<GameObject> shotsPositions;
    public Vector2 shotsDirection;



    public GameObject ammo;
    public GameObject crosshair;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1.0f / shotsPerSeconds;
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

        for (int i=0;i<shotsAmmount;i++)
        {
            for(int j=0;j<bulletsPerShot;j++)
            {
                GameObject bullet = Instantiate(ammo, shotsPositions[i].transform.position, Quaternion.identity);
                Quaternion.FromToRotation(Vector3.right, shotsDirection);
                bullet.transform.right = shotsDirection;
               
                float baseDeviation = 0;
                if (bulletsPerShot>1)
                {
                     baseDeviation = j * bulletConeAngle / (bulletsPerShot - 1) - (bulletConeAngle / 2.0f);
                }
                float spreadValue = Random.Range(-shotsSpreadAngle,shotsSpreadAngle);
                float rotationAngle = baseDeviation + spreadValue;
                bullet.transform.RotateAround(shotsPositions[i].transform.position, Vector3.forward,rotationAngle);

                bullet.GetComponent<Bullet>().speed = shotsSpeed;
            }
        }
    }
}
