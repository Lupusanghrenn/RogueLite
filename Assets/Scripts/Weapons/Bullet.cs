using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ScriptableBullet baseBullet;

    private float speed;
    private float speedDecrease;
    private float size;
    private float damage;
    
    private float explosionRadius;
    private float gravityScale;
    private float enemyPenetration;
    
    private float fireDamage;
    private float poisonDamage;
    private float iceDamage;
    private float shockDamage;
    
    private bool goesThroughWalls;
    private bool homingBullet;

    private float horizontalFrequence;
    private float verticalFrequence;
    private float horizontalAmplitude;
    private float verticalAmplitude;

    private Vector3 horizontalMouvement;
    private Vector3 verticalMouvement;
    private float horizontalTimer;
    private float verticalTimer;

    private float timeBeforeDestruction = 1;
    private float destructionTimer;

    public float Speed { get => speed; set => speed = value; }
    public float SpeedDecrease { get => speedDecrease; set => speedDecrease = value; }
    public float Size { get => size; set => size = value; }
    public float Damage { get => damage; set => damage = value; }
    public float ExplosionRadius { get => explosionRadius; set => explosionRadius = value; }
    public float GravityScale { get => gravityScale; set => gravityScale = value; }
    public float EnemyPenetration { get => enemyPenetration; set => enemyPenetration = value; }
    public float FireDamage { get => fireDamage; set => fireDamage = value; }
    public float PoisonDamage { get => poisonDamage; set => poisonDamage = value; }
    public float IceDamage { get => iceDamage; set => iceDamage = value; }
    public float ShockDamage { get => shockDamage; set => shockDamage = value; }
    public bool GoesThroughWalls { get => goesThroughWalls; set => goesThroughWalls = value; }
    public bool HomingBullet { get => homingBullet; set => homingBullet = value; }
    public float HorizontalFrequence { get => horizontalFrequence; set => horizontalFrequence = value; }
    public float VerticalFrequence { get => verticalFrequence; set => verticalFrequence = value; }
    public float HorizontalAmplitude { get => horizontalAmplitude; set => horizontalAmplitude = value; }
    public float VerticalAmplitude { get => verticalAmplitude; set => verticalAmplitude = value; }
    public float TimeBeforeDestruction { get => timeBeforeDestruction; set => timeBeforeDestruction = value; }



    // Start is called before the first frame update
    void Start()
    {
        destructionTimer = timeBeforeDestruction;
    }

    // Update is called once per frame
    void Update()
    {
        destructionTimer -= Time.deltaTime;
        if(destructionTimer<=0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        horizontalTimer += HorizontalFrequence * Time.deltaTime;
        verticalTimer += VerticalFrequence * Time.deltaTime;
        horizontalMouvement =  transform.up * Mathf.Sin(horizontalTimer) * HorizontalAmplitude;
        verticalMouvement =  transform.right * Mathf.Sin(verticalTimer) * VerticalAmplitude;


        transform.position += verticalMouvement+horizontalMouvement + (transform.right * Speed) * Time.fixedDeltaTime;
        Speed *= SpeedDecrease;
    }
}
