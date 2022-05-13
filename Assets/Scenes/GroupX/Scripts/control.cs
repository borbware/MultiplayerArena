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

        if (playerController && collider.tag == "Projectile")
        {
            playerController.jumpInput = true;
            playerController = null;
            GetComponent<Rotate>().enabled = true;
            player.GetComponent<PlatformerController>().enabled = true;
        }
    }

    void Update()
    {
        if (playerController)
        {
            transform.Rotate(
                new Vector3(0, playerController.axisInput.x * 100 * Time.deltaTime, 0),
                Space.World
            );

            transform.Rotate(
                new Vector3(
                    playerController.axisInput.y * 100 * Time.deltaTime,
                    0, 0), Space.Self
            );

            Vector3 currentRotation = transform.rotation.eulerAngles;
            Debug.Log(currentRotation);
            currentRotation.x = Mathf.Clamp(currentRotation.x, 340, 359);
            transform.localRotation = Quaternion.Euler(currentRotation);

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
