using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float time;
    float Spawntime;

    [SerializeField]
    GameObject[] LaserPatterns;

    [SerializeField]
    GameObject[] SpawnPoint;

    GameObject SpawnedObject;

    Vector3[] Positions = new Vector3[4];

    float SpawnRate = 2.5f;

    int RngSpawn; 
    void Start()
    {
        time = StageManager.instance.stageTime;
        Spawntime = time;

        for (int i=0; i<4;i++){
            Positions[i] = SpawnPoint[i].transform.position;
            Debug.Log(Positions[i]);
        }
    }

    void Update()
    {
        
    }

    void SpawnLaser(){
        if (StageManager.instance.stageState == StageManager.StageState.Play)
        {
            time -= Time.deltaTime;
            if (time < (Spawntime - SpawnRate)){
                Spawntime = time;
                RngSpawn = Random.Range(0, LaserPatterns.Length);
                SpawnedObject = Instantiate(LaserPatterns[RngSpawn]);
                Destroy(SpawnedObject, 5);
            }
        }
    }
}
