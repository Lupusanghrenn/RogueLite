using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New PlayerStat",menuName="PlayerStat")]
public class PlayerStat : ScriptableObject
{
    //Pure UI data
    public string Name;
    public string Description;
    public Sprite Sprite;


    //Pure gameplay stat
    public float Health;
    public float Speed;
    public float JumpHeight;
    public float Attack;
    public float RateOfFire;
    public float Luck;
    public float Lifesteal;
    public int NumberOfJump;

    public void Modifier(StatModifier sm){
        Health+=sm.Health;
        Speed+=sm.Speed;
        JumpHeight+=sm.JumpHeight;
        Attack+=sm.Attack;
        RateOfFire+=sm.RateOfFire;
        Luck+=sm.Luck;
        Lifesteal+=sm.Lifesteal;
        NumberOfJump+=sm.NumberOfJump;
    }

    public void Print(){
        Debug.Log(Name+ " : "+Description+
        "\nHealth : "+Health+
        "\nSpeed : "+Speed+
        "\nJumpHeight : "+JumpHeight+
        "\nAttack : "+Attack+
        "\nRateOfFire : "+RateOfFire+
        "\nLuck : "+Luck+
        "\nLifesteal : "+Lifesteal+
        "\nNumber of jump : "+NumberOfJump);
    }
}
