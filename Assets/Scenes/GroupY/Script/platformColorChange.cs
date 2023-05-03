using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupY{
public class platformColorChange : MonoBehaviour
{
    [SerializeField] GameObject players;
    [SerializeField] Material color;

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
    void OnCollisionEnter(Collision other) 
        {
        if (other.gameObject.tag == "Player")
            {
            gameObject.GetComponent<Renderer>().material.color = 
            other.gameObject.GetComponent<Renderer>().material.color;
            }    
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}