using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    bool KnockedBack;

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
        }
    }

    
    void OnCollisionEnter(Collision C){
        if(C.gameObject.tag == "Projectile" && KnockedBack == false){
            rb = C.gameObject.GetComponent<Rigidbody>();
            KnockbackDir = rb.transform.forward;
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
