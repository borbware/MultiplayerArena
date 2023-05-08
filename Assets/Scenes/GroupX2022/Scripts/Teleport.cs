using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    void Start() 
    {
        Destroy(transform.parent.gameObject, 3.0f);    
    }

    void OnTriggerEnter(Collider collider)
    {
        var obj = collider.gameObject;

        if ((obj.tag == "Player" || obj.tag == "Projectile"))
        // if ((obj.tag == "Player" || obj.tag == "Projectile") && obj.GetComponent<Player>().state == Player.PlayerState.Active)
        {
            string port = null;

            if(gameObject.tag == "Port1")
                port = "Port2";
            else if(gameObject.tag == "Port2")
                port = "Port1";

            if(port != null)
            {
                var otherPort = GameObject.FindGameObjectWithTag(port);

                Rigidbody rb = obj.GetComponent<Rigidbody>();

                rb.MovePosition(otherPort.transform.position);

                Destroy(otherPort.transform.parent.gameObject);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
