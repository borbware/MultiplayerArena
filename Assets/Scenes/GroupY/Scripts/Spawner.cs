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

    int PatternSpawn;

    float[] Rota = {180f, 0, 270f, 90f};

    GameObject SpawnedObject;

    public float StartTime;

    float SpawnerRate;

    Vector3[] Positions = new Vector3[4];

    float[] SpawnRates = {1.3f, 1.5f, 2f, 2.8f, 3.6f, 4.5f, 5.5f, 6.5f};
    float[] Theresholds = {15f, 30f, 45f ,60f, 75f, 90f, 105f, 121f};

    float SpawnRate = 6.5f;

    int RngSpawn; 
    void Start()
    {
        StartTime = Time.time;
        time = StageManager.instance.stageTime;
        Spawntime = time + (SpawnRate - 3.5f);

        for (int i=0; i<4;i++){
            Positions[i] = SpawnPoint[i].transform.position;
        }

    }

    void Update()
    {
        SpawnLasers();
    }

    void SpawnLasers(){
        if (StageManager.instance.stageState == StageManager.StageState.Play)
        {
            time -= Time.deltaTime;
            if (time < (Spawntime - SpawnRate)){
                Spawntime = time;
                SpawnRate = GetSpawnRate();
                PatternSpawn = Random.Range(0, 20);

                if (PatternSpawn < 7){
                    RngSpawn = Random.Range(0, 4);
                    SpawnedObject = Instantiate(LaserPatterns[0], Positions[RngSpawn], 
                    Quaternion.Euler(0, Rota[RngSpawn], 0));} 
                else if (PatternSpawn < 12)
                    DualPattern();
                else if (PatternSpawn < 17)
                    DualPattern2();
                else if (PatternSpawn < 19)
                    TripplePattern();
                else
                    QuadPattern();
            }
        }
    }

    float GetSpawnRate(){
        float RetVal = 6.5f;
        for (int j = 0; j < SpawnRates.Length; j++){
            if(StageManager.instance.stageTime <  Theresholds[j]){
                RetVal = SpawnRates[j];
                break;
            }
        }
        return RetVal;
    }

    void QuadPattern(){
        if(StageManager.instance.stageTime <= 60){
            for (int j = 0; j < 4; j++){
                Instantiate(LaserPatterns[0], Positions[j], 
                Quaternion.Euler(0, Rota[j], 0));
            }
        } else{
            DualPattern();
        }
    }

    void TripplePattern(){
        if(StageManager.instance.stageTime <= 75){
            RngSpawn = Random.Range(0, 4);
            for (int j = 0; j < 3; j++){
                if (RngSpawn + j > 3){
                RngSpawn -= 3;}
                Instantiate(LaserPatterns[0], Positions[RngSpawn + j], 
                Quaternion.Euler(0, Rota[RngSpawn + j], 0));
            }
        } else {
            DualPattern2();
        }
    }

    void DualPattern(){
        RngSpawn = Random.Range(0, 4);
        int j = 1;
        if (RngSpawn > 2) j = -1;

            Instantiate(LaserPatterns[0], Positions[RngSpawn], 
                Quaternion.Euler(0, Rota[RngSpawn], 0));

            Instantiate(LaserPatterns[0], Positions[RngSpawn + j], 
                Quaternion.Euler(0, Rota[RngSpawn + j], 0));
    }

    void DualPattern2(){
         RngSpawn = Random.Range(0, 4);
        int j = 2;
        if (RngSpawn >= 2) j = -2;

            Instantiate(LaserPatterns[0], Positions[RngSpawn], 
                Quaternion.Euler(0, Rota[RngSpawn], 0));

            Instantiate(LaserPatterns[0], Positions[RngSpawn + j], 
                Quaternion.Euler(0, Rota[RngSpawn + j], 0));
    }
}
