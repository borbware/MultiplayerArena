using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpotlightPositionScript : MonoBehaviour
{
    public GameObject movingSpotlightPrefab;
    GameObject movingLight;
    bool HasSpawned = false;
    public Vector3 initialPositionOfLight = new Vector3(0f, 8.5f, 0f);

    private Vector3 pos1 = new Vector3(-5f, 8.5f, 0f);
    private Vector3 pos2 = new Vector3(0f, 8.5f, 5f);
    private Vector3 pos3 = new Vector3(5f, 8.5f, 0f);
    private Vector3 pos4 = new Vector3(0f, 8.5f, -5f);

    private float lightSpeed = 2f;
    private float lerpTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (StageManager.instance.stageTime <= 115f && HasSpawned == false) {
            HasSpawned = true;
            SpotLightSpawnAndMove();
        }
        
    }

    public void SpotLightSpawnAndMove() 
    {
            movingLight = Instantiate(movingSpotlightPrefab, initialPositionOfLight, Quaternion.Euler(90f, 0f, 0f));

            if (StageManager.instance.stageTime > 0.0f && lerpTime < 1f) {
                
                lerpTime += Time.deltaTime * lightSpeed;
                movingLight.transform.position = Vector3.Lerp(initialPositionOfLight, pos1, lerpTime);
                
            }
            
    }

}
