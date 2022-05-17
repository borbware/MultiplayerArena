using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpotlightPositionScript : MonoBehaviour
{
    public GameObject movingSpotlightPrefab;
    GameObject movingLight;
    bool HasSpawned = false;
    private Vector3 initialPositionOfLight = new Vector3(0f, 8.5f, 0f);
    
    private Vector3 pos1 = new Vector3(-2.8f, 8.5f, 0f);
    private Vector3 pos2 = new Vector3(0f, 8.5f, 2.8f);
    private Vector3 pos3 = new Vector3(2.8f, 8.5f, 0f);
    private Vector3 pos4 = new Vector3(0f, 8.5f, -2.8f);

    AudioClip _audio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (StageManager.instance.stageTime <= 90.0f && HasSpawned == false) {
            HasSpawned = true;
            SpotLightSpawnAndMove();
            
        }
        
    }

    

    IEnumerator MoveSpotlight() 
    {
        while (true) {
            yield return new WaitForSeconds(1);
            yield return LerpFunction.LerpPosition(movingLight.transform, pos1, 2.5f);
            yield return new WaitForSeconds(0.2f);
            yield return LerpFunction.LerpPosition(movingLight.transform, pos2, 2.5f);
            yield return new WaitForSeconds(0.2f);
            yield return LerpFunction.LerpPosition(movingLight.transform, pos3, 2.5f);
            yield return new WaitForSeconds(0.2f);
            yield return LerpFunction.LerpPosition(movingLight.transform, pos4, 2.5f);
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void SpotLightSpawnAndMove() 
    {
        movingLight = Instantiate(movingSpotlightPrefab, initialPositionOfLight, Quaternion.Euler(90f, 0f, 0f)); 
        StartCoroutine(MoveSpotlight());

        
        
    }

}
