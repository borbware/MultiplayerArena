using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDropEvent : MonoBehaviour
{
    float timeTillDrop = 3f;
    float totalStageTime;
    bool eventTriggered = false;

    void eventTrigger(){
        float currentStageTime = GameObject.Find("StageManager").GetComponent<StageManager>().stageTime;
        if (totalStageTime - currentStageTime > timeTillDrop && !eventTriggered){  
            eventTriggered = true;
            GetComponent<MoleSpawner>().listOfHoles.RemoveRange(7, 6);  //removes 6 elements starting from index 7 (these are the last 6 holes)
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
        eventTrigger();
    }
}
