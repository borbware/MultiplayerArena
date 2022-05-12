using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardishot : MonoBehaviour
{
    int shotPower;

    // Hazard's firing effect
    private void OnTriggerStay(Collider col) {
        col.attachedRigidbody.AddForce(-transform.up * Time.deltaTime * shotPower);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotPower = transform.GetComponentInParent<hazardi>().shotPower;
    }
}
