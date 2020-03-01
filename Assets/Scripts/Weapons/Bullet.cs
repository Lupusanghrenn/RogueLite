using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ScriptableBullet baseBullet;

    public float speed;
    public float speedDecrease;
    public float size;
    public float damage;

    public float explosionRadius;
    public float gravityScale;
    public float enemyPenetration;

    public float fireDamage;
    public float poisonDamage;
    public float iceDamage;
    public float shockDamage;

    public bool goesThroughWalls;
    public bool homingBullet;

    public float horizontalSpeed;
    public float verticalSpeed;
    private Vector3 horizontalMouvement;
    private Vector3 verticalMouvement;
    private float horizontalTimer = 0;
    private float verticalTimer = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        horizontalTimer += horizontalSpeed * Time.deltaTime;
        verticalTimer += verticalSpeed * Time.deltaTime;
        horizontalMouvement =  transform.up * Mathf.Cos(horizontalTimer)*0.05f;
        verticalMouvement =  transform.right * Mathf.Sin(verticalTimer)*0.05f;


        transform.position += verticalMouvement+horizontalMouvement + (transform.right * speed) * Time.fixedDeltaTime;
        speed *= speedDecrease;
    }
}
