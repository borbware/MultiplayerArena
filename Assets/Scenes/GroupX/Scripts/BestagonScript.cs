using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestagonScript : MonoBehaviour
{
    float timeTillDrop = 3f;
    float totalStageTime;
    void dropFloor(){
        float currentStageTime = GameObject.Find("StageManager").GetComponent<StageManager>().stageTime;
        if (totalStageTime - currentStageTime > timeTillDrop){  
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;  
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        totalStageTime = GameObject.Find("StageManager").GetComponent<StageManager>().stageTime;
    }

    // Update is called once per frame
    void Update()
    {
        dropFloor();
    }
}
