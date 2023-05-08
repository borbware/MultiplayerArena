using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    GameObject player;
    float timer = 0;
    bool started = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            player = collider.gameObject;

            var movement = player.GetComponent<PlatformerController>();
            var shooter = player.GetComponent<ShootProjectile>();

            movement.maxSpeed += 5;
            movement.maxAcceleration += 5;
            movement.jumpHeight += 1;
            shooter.shootForce += 5000;
            shooter.shootPeriod -= 0.4f;

            timer = 15;
            started = true;

            GetComponent<Collider>().enabled = false;
            Destroy(transform.Find("Box001").gameObject);
        }
    }

    void Update()
    {
        if (started)
        {
            timer -= Time.deltaTime;

            if (player == null)
            {
                Destroy(gameObject);
                return;
            }
            transform.position = player.transform.position;

            if (timer <= 0)
            {
                var movement = player.GetComponent<PlatformerController>();
                var shooter = player.GetComponent<ShootProjectile>();

                movement.maxSpeed -= 5;
                movement.maxAcceleration -= 5;
                movement.jumpHeight -= 1;
                shooter.shootForce -= 5000;
                shooter.shootPeriod += 0.4f;

                Destroy(gameObject);
            }

        }
    }
}
