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

    void QuadPattern(){
        for (int j = 0; j < 4; j++){
            Instantiate(LaserPatterns[0], Positions[j], 
            Quaternion.Euler(0, Rota[j], 0));
        }
    }

    void TripplePattern(){
        RngSpawn = Random.Range(0, 4);

        for (int j = 0; j < 3; j++){
            if (RngSpawn + j > 3){
                RngSpawn -= 3;}
            Instantiate(LaserPatterns[0], Positions[RngSpawn + j], 
            Quaternion.Euler(0, Rota[RngSpawn + j], 0));
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
