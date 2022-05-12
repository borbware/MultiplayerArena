using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpotlightPositionScript : MonoBehaviour
{

    
    public Vector3 InitialPositionOfLight;

    private Vector3 ValueOne;
    private Vector3 ValueTwo;

    private Vector3 RandomVector;

    Rigidbody rb;

    float moveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        InitialPositionOfLight = new Vector3(0f, 7f, 0f);
        gameObject.transform.position = InitialPositionOfLight;

        rb = GetComponent<Rigidbody>();

        ValueOne = new Vector3(4f, 0f, -4f);
        ValueTwo = new Vector3(2f, 0f, -2f);
        RandomVector = new Vector3(Mathf.PingPong(2f, -4f), Mathf.PingPong(1f, 2f));
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(RandomVector, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);
    }

}
