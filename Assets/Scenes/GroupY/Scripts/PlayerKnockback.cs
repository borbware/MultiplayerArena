using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    bool KnockedBack;

    float[] KnockDir = {1f,1f};

    float KnockbackStrength;


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
                KnockbackDir * KnockbackStrength * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter(Collider C){
        if(C.gameObject.tag == "Projectile" && KnockedBack == false){
            KnockbackStrength = 3650f;
            LuotiOsuma(C.gameObject, 0.15f);
        } else if(C.gameObject.tag == "AutoShot" && KnockedBack == false){
            KnockbackStrength = 5050f;
            LuotiOsuma(C.gameObject, 0.11f);
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
