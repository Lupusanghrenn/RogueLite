using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    private RogueLiteInputAction controls;
    public PlayerStat Stats;
    public StatModifier sm1,sm2;
    protected new Rigidbody2D rigidbody;
    private Vector2 moveAxis;
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
        rigidbody = GetComponent<Rigidbody2D>(); 
        controls= new RogueLiteInputAction();
        controls.Enable();
    }

    public void OnMove(InputValue value){
        Debug.Log(value);
        moveAxis=value.Get<Vector2>();
    }

    public void OnFire(){
        Debug.Log("Fire");
        //Weapon.Fire();
    }

    public void OnJump(){
        Debug.Log("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        //rigidbody.MovePosition(moveAxis);
        transform.position+=new Vector3(moveAxis.x * Stats.Speed*Time.deltaTime,moveAxis.y * Stats.Speed*Time.deltaTime,0);
    }
}
