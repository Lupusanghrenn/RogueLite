using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float size;
    public float damage;


    public float explosionRadius;
    public float gravityScale;
    public float enemyPenetration;

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
        transform.position += transform.right * speed * Time.fixedDeltaTime;
    }
}
