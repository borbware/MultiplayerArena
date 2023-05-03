using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestagonScript : MonoBehaviour
{
    float timeTillDrop = 5f;
    void dropFloor(){
        if (Time.time >= timeTillDrop){
            
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dropFloor();
    }
}
