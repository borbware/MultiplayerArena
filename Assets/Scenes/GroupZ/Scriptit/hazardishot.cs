using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardishot : MonoBehaviour
{
    [SerializeField] int shotPower = 2500;
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
        
    }
}
