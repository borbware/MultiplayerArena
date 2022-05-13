using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpotlightPositionScript : MonoBehaviour
{
    public GameObject movingSpotlightPrefab;
    GameObject movingLight;
    bool HasSpawned = false;
    private Vector3 initialPositionOfLight = new Vector3(0f, 8.5f, 0f);
    
    private Vector3 pos1 = new Vector3(-5f, 8.5f, 0f);
    private Vector3 pos2 = new Vector3(0f, 8.5f, 5f);
    private Vector3 pos3 = new Vector3(5f, 8.5f, 0f);
    private Vector3 pos4 = new Vector3(0f, 8.5f, -5f);

    public float time;

    LerpFunction _lerpfunction;
    

    // Start is called before the first frame update
    void Start()
    {
        time = StageManager.instance.stageTime;
        _lerpfunction = GetComponent<LerpFunction>();
    }

    // Update is called once per frame
    void Update()
    {

        if (StageManager.instance.stageTime <= 115.0f && HasSpawned == false) {
            HasSpawned = true;
            SpotLightSpawnAndMove();
        }
        
    }

    public void BoxPosition()
    {
        if (!movingLight) {
            Debug.Log("No movable object found.");
            return;
        }

        if (time <= 115f && time > 110f) {
            _lerpfunction.LerpPosition(pos1, 3f);
        } 

        else if (time <= 105f && time > 100f) {
            _lerpfunction.LerpPosition(pos2, 3f);
        }

        else if (time <= 95f && time > 90f) {
            _lerpfunction.LerpPosition(pos3, 3f);
        }

        else if (time <= 85f && time > 95f) {
            _lerpfunction.LerpPosition(pos4, 3f);
        }
    }

    public void SpotLightSpawnAndMove() 
    {
        movingLight = Instantiate(movingSpotlightPrefab, initialPositionOfLight, Quaternion.Euler(90f, 0f, 0f)); 
        _lerpfunction.LerpPosition(initialPositionOfLight, 3f);
        BoxPosition();
    }

}
