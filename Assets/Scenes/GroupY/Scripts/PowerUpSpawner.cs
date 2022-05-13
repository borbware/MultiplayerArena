using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject PowerUp;

    float Spawnrate = 15f;

    float LastSpawn = 0f;
    GameObject SpawnedPowerUp;

    Vector3 SpawnPoint;

    int RandomSpawn;

    GameObject[] obj;

    void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Platform");
        LastSpawn = Time.time;
    }

    void Update()
    {
        if(Time.time > LastSpawn + Spawnrate){
            LastSpawn = Time.time;
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp(){
        RandomSpawn = Random.Range(0, obj.Length);
        SpawnPoint = new Vector3
        (obj[RandomSpawn].transform.position.x, 40f, obj[RandomSpawn].transform.position.z);
        SpawnedPowerUp = Instantiate(PowerUp,SpawnPoint,Quaternion.identity);
        Destroy(SpawnedPowerUp, 30);
    }
}
