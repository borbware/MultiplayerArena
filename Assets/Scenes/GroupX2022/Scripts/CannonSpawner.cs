using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawner : MonoBehaviour
{
    private GameObject currentCannon = null;
    private float timer = 0;

    void Update()
    {
        if (StageManager.instance.stageState == StageManager.StageState.Play)
        {
            if (currentCannon == null)
            {
                currentCannon = transform.Find("Cannon" + Random.Range(1, 5)).gameObject;
                currentCannon.SetActive(true);
                timer = Random.Range(10, 10);
            }

            if (timer <= 0)
            {
                if (currentCannon.GetComponent<control>().playerController)
                {
                    var player = currentCannon.GetComponent<control>().playerController;

                    player.transform.position = new Vector3(
                        currentCannon.GetComponent<control>().seat.transform.position.x,
                        0.5f,
                        currentCannon.GetComponent<control>().seat.transform.position.z
                    );

                    player.GetComponent<PlatformerController>().enabled = true;
                    player.GetComponent<ShootProjectile>().enabled = true;
                    currentCannon.GetComponent<Rotate>().enabled = true;

                    currentCannon.GetComponent<control>().playerController = null;
                    currentCannon.GetComponent<control>().player = null;
                }

                currentCannon.transform.rotation = Quaternion.Euler(0, 0, 0);
                currentCannon.SetActive(false);
                currentCannon = transform.Find("Cannon" + Random.Range(1, 5)).gameObject;
                currentCannon.SetActive(true);
                timer = Random.Range(10, 10);
            }

            timer -= Time.deltaTime;
        }
    }
}
