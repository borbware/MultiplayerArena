using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpawner : MonoBehaviour
{
    public GameObject teleportPrefab;

    private float timer = 0;


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && StageManager.instance.stageState == StageManager.StageState.Play)
        {
            GameObject t1 = Instantiate(teleportPrefab, new Vector3(
                Random.Range(3.5f, -3.5f),
                0.5f,
                Random.Range(3.5f, -3.5f)
            ), Quaternion.Euler(0, 0, 0));

            t1.transform.Find("Model").tag = "Port1";

            var t2 = Instantiate(teleportPrefab, new Vector3(
                Random.Range(3.5f, -3.5f),
                0.5f,
                Random.Range(3.5f, -3.5f)
            ), Quaternion.Euler(0, 0, 0));

            t2.transform.Find("Model").tag = "Port2";

            timer = Random.Range(3, 8);
        }
    }
}
