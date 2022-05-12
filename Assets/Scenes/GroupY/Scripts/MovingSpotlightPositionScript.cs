using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpotlightPositionScript : MonoBehaviour
{

    
    private Vector3 InitialPositionOfLight;
    private Vector3 positionDisplacement;

    // float moveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {

        InitialPositionOfLight = transform.position;
        float randomDistance = Random.Range(5f, -5f);
        positionDisplacement = new Vector3(randomDistance, randomDistance, randomDistance);
        
    }

    // Update is called once per frame
    void Update()
    {
        StageManager.instance.stageTime += Time.deltaTime;
        transform.position = Vector3.Lerp(InitialPositionOfLight, InitialPositionOfLight + positionDisplacement, Mathf.PingPong(StageManager.instance.stageTime, 2));
    }

}
