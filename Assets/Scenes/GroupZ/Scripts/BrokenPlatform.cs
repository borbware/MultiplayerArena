using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{
    Rigidbody rb;
    Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotation.x = Random.Range(-20, 20);
        rotation.y = Random.Range(-20, 20);
        rotation.z = Random.Range(-20, 20);
        rb.AddTorque(rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
