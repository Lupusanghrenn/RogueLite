using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New PlayerStat",menuName="PlayerStat")]
public class PlayerStat : ScriptableObject
{
    public float Health;
    public float Speed;
    public float JumpHeight;
    public float Attack;
    public float RateOfFire;
    public float Luck;
    public float Lifesteal;
}
