using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBalls : MonoBehaviour
{
    private float timer = 0;

    void OnCollisionEnter(Collision bullet)
    {
        if (bullet.gameObject.tag.Equals("Projectile"))
        {
            Destroy (bullet.gameObject, 0.1f);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3)
        {
            Destroy(gameObject);
        }
    }
}