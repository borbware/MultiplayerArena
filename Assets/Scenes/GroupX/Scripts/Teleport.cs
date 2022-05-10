using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        var obj = collider.gameObject;
        if (obj.tag == "Player" && obj.GetComponent<Player>().state == Player.PlayerState.Active)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.MovePosition(new Vector3(
                Random.Range(3.5f, -3.5f),
                0.5f,
                Random.Range(3.5f, -3.5f)
            ));

            Destroy(gameObject);
        }

        if(obj.tag == "Projectile")
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            
            rb.MovePosition(new Vector3(Random.Range(3.5f, -3.5f), 0.5f, Random.Range(3.5f, -3.5f)));

            Destroy(gameObject);
        }
    }
}
