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
        if (timer <= 0)
        {
            Instantiate(teleportPrefab, new Vector3(
                Random.Range(3.5f, -3.5f),
                0.5f,
                Random.Range(3.5f, -3.5f)
            ), Quaternion.Euler(0, 0, 0));

            timer = Random.Range(3, 8);
        }
    }
}
