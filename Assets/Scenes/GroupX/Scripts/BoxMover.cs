using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    const float VALUE = 3.5f;

    float x, z;
    
    Vector3 speed;


    // Start is called before the first frame update
    void Start()
    {
        x = 0.0f;
        z = VALUE;

        speed = Vector3.right;

        transform.position = new Vector3(x, 0.0f, z);
    }

    // Update is called once per frame
    void Update()
    {
        if(StageManager.instance.stageState == StageManager.StageState.Play)
        {
            x = transform.position.x;
            z = transform.position.z;

            if( x > VALUE && speed == Vector3.right || 
                x < -VALUE && speed == Vector3.left ||
                z < -VALUE && speed == Vector3.back || 
                z > VALUE && speed == Vector3.forward)
                    speed.Set(speed.z, 0.0f, -speed.x);

            transform.position += speed * Time.deltaTime;
        }
    }
}
