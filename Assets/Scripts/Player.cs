using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerStat Stats;
    public StatModifier sm1,sm2;
    // Start is called before the first frame update
    void Awake()
    {
        //TODO Randomly generate sm (using Resources.Load)
        sm1=Resources.Load<StatModifier>("StatModifier/Test1");
        sm2=Resources.Load<StatModifier>("StatModifier/Test2");
        Stats.Modifier(sm1);
        Stats.Modifier(sm2);

        GetComponent<SpriteRenderer>().sprite=Stats.Sprite;

        Stats.Print();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
