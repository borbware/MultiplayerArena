using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public GameObject seat;

    private Player playerController = null;
    private GameObject player;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playerController = collider.GetComponent<Player>();
            GetComponent<Rotate>().enabled = false;
            player = collider.gameObject;
            player.GetComponent<PlatformerController>().enabled = false;
        }
    }

    void Update()
    {
        if (playerController)
        {
            transform.Rotate(new Vector3(0, playerController.axisInput.x * 100 * Time.deltaTime, 0));
            player.transform.position = seat.transform.position;

            if (playerController.jumpInput)
            {
                playerController = null;
                GetComponent<Rotate>().enabled = true;
                player.GetComponent<PlatformerController>().enabled = true;
            }
        }
    }
}
