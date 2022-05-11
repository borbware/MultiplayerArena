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

    float[] Rota = {180f, 0, 270f, 90f};

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
        }

    }

    void Update()
    {
        SpawnLaser();
    }

    void SpawnLaser(){
        if (StageManager.instance.stageState == StageManager.StageState.Play)
        {
            time -= Time.deltaTime;
            if (time < (Spawntime - SpawnRate)){
                Spawntime = time;
                RngSpawn = Random.Range(0, 4);
                SpawnedObject = Instantiate(LaserPatterns[0], Positions[RngSpawn], 
                Quaternion.Euler(0, Rota[RngSpawn], 0));
                Destroy(SpawnedObject, 5);
            }
        }
    }
}
