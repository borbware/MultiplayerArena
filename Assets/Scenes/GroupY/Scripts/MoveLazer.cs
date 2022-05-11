using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLazer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            GetComponent<Rigidbody>()
        .AddForce(transform.right * 40);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward
    }
}
