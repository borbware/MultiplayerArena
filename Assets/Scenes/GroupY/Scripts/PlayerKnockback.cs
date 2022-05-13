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
            LuotiOsuma(C.gameObject, 0.15f);
        } else if(C.gameObject.tag == "AutoShot" && KnockedBack == false){
            LuotiOsuma(C.gameObject, 0.085f);
        }

    }
    

    void LuotiOsuma (GameObject Object, float f){
        rb = Object.GetComponent<Rigidbody>();
            //Debug.Log(C.gameObject.name);
            KnockbackDir = rb.velocity.normalized;
            Destroy(Object);
            KnockedBack = true;
            Invoke("CancelKnockback", f);
    }

    void CancelKnockback(){
        KnockedBack = false;
    }
}
