using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDropEvent : MonoBehaviour
{
    float timeTillDrop = 5f;
    float totalStageTime;
    bool eventTriggered = false;
    bool warningSounded = false;
    
    [SerializeField] AudioSource dropWarningAudio;
    [SerializeField] AudioSource floorDropAudio;
    [SerializeField] List<GameObject> outerHexes;

    StageManager stageManager;

    void eventTrigger(){
        float currentStageTime = stageManager.stageTime;

        //warning sounds
        if (totalStageTime - currentStageTime >= timeTillDrop - 2.9 && !warningSounded){
            warningSounded = true;
            InvokeRepeating("playWarningSound", 0.1f, 1f);
        }

        //the event
        if (totalStageTime - currentStageTime > timeTillDrop && !eventTriggered){  
            eventTriggered = true;

            //remove the extra moleholes - removes 6 elements starting from index 7 (these are the last 6 holes)
            GetComponent<MoleSpawner>().listOfHoles.RemoveRange(7, 6);

            //drop outer hexes
            floorDropAudio.Play();
            foreach (GameObject hex in outerHexes)
            {
                hex.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;  
                hex.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    void playWarningSound(){
        if (!eventTriggered){dropWarningAudio.Play();}   
    }

    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        totalStageTime = stageManager.stageTime;
    }

    // Update is called once per frame
    void Update()
    {
        eventTrigger();
    }
}
