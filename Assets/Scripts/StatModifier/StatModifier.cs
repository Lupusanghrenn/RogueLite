using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New StatModifier",menuName="StatModifier")]
public class StatModifier : ScriptableObject
{
    public string Name;
    //Pure gameplay stat
    public float Health;
    public float Speed;
    public float JumpHeight;
    public float Attack;
    public float RateOfFire;
    public float Luck;
    public float Lifesteal;
    public int NumberOfJump;
}
