using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject PowerUp, Heal;

    float Spawnrate = 15f;

    float LastSpawn = 0f;
    GameObject SpawnedPowerUp;

    Vector3 SpawnPoint;

    int RandomSpawn;

    GameObject[] obj;
    Vector3[] Positions;

    bool SpawnPointsSet = false;

    void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Platform");
        LastSpawn = Time.time;
    }

    void Update()
    {
        if(SpawnPointsSet == false){
            SpawnPointsSet = true;
            Positions = new Vector3[16];
            for (int j = 0; j < obj.Length; j++){
                Positions[j] = obj[j].transform.position;
            }
        }
        if(Time.time > LastSpawn + Spawnrate){
            LastSpawn = Time.time;
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp(){
        GameObject Spawned;
        RandomSpawn = Random.Range(0, obj.Length);
        SpawnPoint = new Vector3
        (Positions[RandomSpawn].x, 40f, Positions[RandomSpawn].z);

        RandomSpawn = Random.Range(0, 5);

        if(RandomSpawn > 2){
            Spawned = Heal;
        } else{
            Spawned = PowerUp;
        }

        SpawnedPowerUp = Instantiate(Spawned,SpawnPoint,Quaternion.Euler(75f,0f,0f));
        Destroy(SpawnedPowerUp, 30);
    }
}
