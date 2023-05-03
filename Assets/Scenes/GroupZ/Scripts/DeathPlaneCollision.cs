using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupZ
{
public class DeathPlaneCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Death")
        {
            Destroy(gameObject);
            var _player = GetComponent<Player>();
            if (_player != null)
                StageManager.instance.AddHP(_player.player, -100);
        }
    }
}
}
