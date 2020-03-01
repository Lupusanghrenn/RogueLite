using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Bullet",menuName = "Weapons/Bullet")]
public class ScriptableBullet : ScriptableObject
{
    //Bullet stats

    public float baseSpeed;
    public float baseSpeedDecrease;
    public float baseSize;
    public float baseDamage;
    public float baseFireDamage;
    public float basePoisonDamage;
    public float baseIceDamage;
    public float baseShockDamage;
    public float baseHorizontalSpeed; // boomerang
    public float baseVerticalSpeed; // sinus
    public float baseExplosionRadius;
    public float baseGravityScale;
    public float baseEnemyPenetration;
    public bool baseWallPenetration;
    public bool baseHoming;   
}
