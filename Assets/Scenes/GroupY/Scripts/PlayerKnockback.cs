using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    bool KnockedBack;

    float[] KnockDir = {1f,1f};

    Rigidbody rb;
    Rigidbody destRB;
    Vector3 KnockbackDir;

    void Start()
    {
        destRB = gameObject.GetComponent<Rigidbody>();        
    }


    void FixedUpdate(){
        if (KnockedBack == true){
            destRB.gameObject.GetComponent<Rigidbody>().AddForce(
                KnockbackDir * 3650 * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter(Collider C){
        if(C.gameObject.tag == "Projectile" && KnockedBack == false){
            rb = C.gameObject.GetComponent<Rigidbody>();
            //Debug.Log(C.gameObject.name);
            KnockbackDir = rb.velocity.normalized;
            Destroy(C.gameObject);
            KnockedBack = true;
            Invoke("CancelKnockback", 0.15f);
        }
    }

    void CancelKnockback(){
        KnockedBack = false;
    }
}
