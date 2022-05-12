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
            //destRB.gameObject.GetComponent<Rigidbody>().AddForce(
        //KnockbackDir * 1200 * Time.fixedDeltaTime);
        //gameObject.transform.position
        }
    }

    
    void OnCollisionEnter(Collision C){
        if(C.gameObject.tag == "Projectile" && KnockedBack == false){
            rb = C.gameObject.GetComponent<Rigidbody>();
            KnockbackDir = rb.transform.forward;
            KnockDir[0] = -(transform.position.x - C.gameObject.transform.position.x);
            KnockDir[1] = -(transform.position.y - C.gameObject.transform.position.y);
            Debug.Log(KnockbackDir);
            Destroy(C.gameObject);
            KnockedBack = true;
            Invoke("CancelKnockback", 0.2f);
        }
    }

    void CancelKnockback(){
        KnockedBack = false;
    }
}
