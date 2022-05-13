using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_falling_script : MonoBehaviour
{
    GameObject[] platforms;
    bool hasbeendropped = false;



    void Start()
    {
       platforms = GameObject.FindGameObjectsWithTag("Platform");

    }

    IEnumerator StartDrop()
    {
        Debug.Log("vaihda väri");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("tiputa");
    }


    void Update()
    {
       if(Time.time >2.0f && hasbeendropped == false) {
           StartCoroutine(StartDrop()); 
           hasbeendropped = true;
       }
    }
}
