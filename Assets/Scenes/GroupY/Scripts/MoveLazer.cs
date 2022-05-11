using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLazer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float Speed;
    void Start()
    {   Destroy(gameObject, 5);
        Speed = (Speed * (1 + (StageManager.instance.stageTime / 60)));
        Debug.Log(Speed);

        GetComponent<Rigidbody>()
        .AddForce(transform.right * Speed);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward
    }
}
