using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpotlightPositionScript : MonoBehaviour
{
    public GameObject movingSpotlightPrefab;
    public Vector3 initialPositionOfLight = new Vector3(0f, 8.5f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (StageManager.instance.stageState == StageManager.StageState.Play && StageManager.instance.stageTime == 115f) {

            SpotLightSpawn();

        }
        
    }

    public void SpotLightSpawn() 
    {

            Instantiate(movingSpotlightPrefab, initialPositionOfLight, Quaternion.identity);
 
    }

}
