using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float time;
    float Spawntime;

    [SerializeField]
    GameObject[] LaserPatterns;

    GameObject SpawnedObject;

    float SpawnRate = 2.5f;

    int RngSpawn; 
    void Start()
    {
        time = StageManager.instance.stageTime;
        Spawntime = time;
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
            }
        }
    }
}
