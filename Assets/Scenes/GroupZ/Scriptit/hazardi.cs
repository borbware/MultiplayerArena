using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardi : MonoBehaviour
{
    float time, t;
    [SerializeField] bool lerping, activated;
    Rigidbody wall1, wall2, wall3, wall4, floor1, floor2, floor3, floor4;
    [SerializeField] int rotationTime = 480;
    public int shotPower = 480;

    private void OnTriggerStay(Collider col) {
        col.attachedRigidbody.AddForce(-transform.up * Time.deltaTime * shotPower);
    }
    void Start()
    {
        time = StageManager.instance.stageTime;
        lerping = false;
        activated = false;
        wall1 = GameObject.Find("Wall1").GetComponent<Rigidbody>();
        wall2 = GameObject.Find("Wall2").GetComponent<Rigidbody>();
        wall3 = GameObject.Find("Wall3").GetComponent<Rigidbody>();
        wall4 = GameObject.Find("Wall4").GetComponent<Rigidbody>();
        floor1 = GameObject.Find("Floor1").GetComponent<Rigidbody>();
        floor2 = GameObject.Find("Floor2").GetComponent<Rigidbody>();
        floor3 = GameObject.Find("Floor3").GetComponent<Rigidbody>();
        floor4 = GameObject.Find("Floor4").GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // When hazard comes up
        if (StageManager.instance.stageTime < time/1.25f && activated != true) {
            activated = true;
            lerping = true;
            t = 0;
        }

        // Hazard coming up movement
        if (lerping) {
            transform.localPosition = Vector3.Lerp(new Vector3(2.5f, -7.5f, 2.2f), new Vector3(2.5f, -0.5f, 2.2f), t);
            t += Time.deltaTime;
            if (transform.localPosition == new Vector3(2.5f, -0.5f, 2.2f)) 
                lerping = false;
        }

        // Hazard starts firing and rotating
        if (!lerping && activated) {
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(0).GetComponent<Collider>().enabled = true;
            transform.Rotate(Vector3.Lerp(new Vector3(0,0,0),new Vector3(0,360,0), t/(rotationTime)), Space.Self);
        }

        // Jos aikaa jäljellä vaan kuudesosa alkup. ajasta
        if (StageManager.instance.stageTime < time/6) {
            rotationTime = 90;
            shotPower = 900;
        }
        // Jos aikaa jäljellä vaan viidesosa alkup. ajasta
        else if (StageManager.instance.stageTime < time/5) {
            
            rotationTime = 120;
            shotPower = 760;
        }
        // Jos aikaa jäljellä vaan neljäsosa alkup. ajasta
        else if (StageManager.instance.stageTime < time/4) {
            wall1.constraints = RigidbodyConstraints.None;
            wall2.constraints = RigidbodyConstraints.None;
            wall3.constraints = RigidbodyConstraints.None;
            wall4.constraints = RigidbodyConstraints.None;
            rotationTime = 180;
            shotPower = 660;
        }
        // Jos aikaa jäljellä vaan kolmasosa alkup. ajasta
        else if (StageManager.instance.stageTime < time/3) {
            rotationTime = 240;
            shotPower = 580;
        }
        // Jos puolet ajasta jäljellä
        else if (StageManager.instance.stageTime < time/2) {
            rotationTime = 360;
            shotPower = 520;
        }
    }
}
