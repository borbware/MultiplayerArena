using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLazer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float Speed;

    float ScaledSpeed;

    float SelectedMultiplier;

    float time;
    void Start()
    {   Destroy(gameObject, 15);
    //Spawner.StartTime
        time = StageManager.instance.stageTime;
        //Speed = (Speed * (1 + ((100 / StageManager.instance.stageTime) / 100)));

        float[] Multiplier = {4.7f, 3.8f, 3f, 2.2f, 2f, 1.7f, 1.3f, 1f};
        float[] Theresholds = {15f, 30f, 45f ,60f, 75f, 90f, 105f, 121f};

        for (int j = 0; j < Theresholds.Length; j++){
            if(StageManager.instance.stageTime <  Theresholds[j]){
                SelectedMultiplier = Multiplier[j];
                break;
            }
        }

        //Debug.Log(Speed * (120 / time));
        GetComponent<Rigidbody>()
        .AddForce(transform.right * (Speed * SelectedMultiplier));//(Speed * (1 + ((100 / time) / 100))));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward
    }
}
