using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMouvement : MonoBehaviour
{

    private Vector3 initialPosition;
    public float distance;
    public float horizontalSpeed;
    public float verticalSpeed;

    private Vector3 horizontalMouvement;
    private Vector3 verticalMouvement;
    private float horizontalTimer=0;
    private float verticalTimer =0;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        horizontalTimer += horizontalSpeed * Time.deltaTime;
        verticalTimer += verticalSpeed * Time.deltaTime;
        horizontalMouvement = new Vector3(Mathf.Cos(horizontalTimer), 0, 0)*distance;
        verticalMouvement = new Vector3(0, Mathf.Sin(verticalTimer), 0)*distance;

        transform.position = initialPosition + verticalMouvement + horizontalMouvement;
    }
}
