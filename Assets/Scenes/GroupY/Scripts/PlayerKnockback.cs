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
    // Start is called before the first frame update
    void Start()
    {
        destRB = gameObject.GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if (KnockedBack == true){
            destRB.gameObject.GetComponent<Rigidbody>().AddForce(
        KnockbackDir * 1200 * Time.fixedDeltaTime);
        //gameObject.transform.position

        //   destRB.MovePosition(
        //          transform.position + new Vector3(
        //              (KnockDir[0]),//FloatDir[0],//-LastInput[0],
        //              (KnockDir[1]),//FloatDir[1], //-LastInput[1],
        //          0f
        //         ) * Time.deltaTime * (20f));
        // }
        }
    }

    
    void OnCollisionEnter(Collision C){
        if(C.gameObject.tag == "Projectile" && KnockedBack == false){
            rb = C.gameObject.GetComponent<Rigidbody>();
            KnockbackDir = rb.velocity.normalized;
            //KnockDir[0] = (transform.position.x - 
            //C.gameObject.transform.position.x);
            //KnockDir[1] = (transform.position.y - 
            //C.gameObject.transform.position.y);
            Debug.Log(KnockbackDir);
            //Destroy(C.gameObject);
            KnockedBack = true;
            Invoke("CancelKnockback", 0.2f);
        }
    }

    void OnTriggerEnter(Collider C){
        if(C.gameObject.tag == "Projectile" && KnockedBack == false){
            rb = C.gameObject.GetComponent<Rigidbody>();
            Debug.Log(C.gameObject.name);
            KnockbackDir = rb.transform.forward;
            Destroy(C.gameObject);
            KnockedBack = true;
            Invoke("CancelKnockback", 0.3f);
        }
    }

    void CancelKnockback(){
        KnockedBack = false;
    }
}
