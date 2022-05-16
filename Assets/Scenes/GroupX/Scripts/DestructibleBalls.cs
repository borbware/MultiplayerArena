using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBalls : MonoBehaviour
{
    void OnCollisionEnter(Collision bullet)
    {
        if (bullet.gameObject.tag.Equals("Projectile"))
        {
            Destroy (bullet.gameObject, 0.1f);
        }
    }
}