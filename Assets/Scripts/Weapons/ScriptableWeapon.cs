using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Weapon")]
public class ScriptableWeapon : ScriptableObject
{
    //Weapon stats

    public float baseShotsPerSeconds;
    public int baseBulletsPerShot;
    public float baseBulletConeAngle;
    public float baseShotsSpreadAngle;
    public int baseShotsAmmount;
    public float baseDamageMultiplier;

    public GameObject ammo;
}
