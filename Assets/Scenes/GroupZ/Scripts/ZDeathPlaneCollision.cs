using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZDeathPlaneCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Death")
        {
            Destroy(gameObject);
            var _player = GetComponent<ZPlayer>();
            if (_player != null)
                StageManager.instance.AddHP(_player.player, -100);
        }
    }
}
