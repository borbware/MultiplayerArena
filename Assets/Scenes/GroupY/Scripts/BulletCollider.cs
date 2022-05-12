using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision C){
        Debug.Log(C.gameObject.tag);
        if(C.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
