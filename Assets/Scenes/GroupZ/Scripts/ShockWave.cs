using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupZ
{
public class ShockWave : MonoBehaviour
{
    public int shooter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player otherPlayer = other.GetComponent<Player>();
            if (otherPlayer.player != shooter)
            {
            StageManager.instance.UIManagers[shooter - 1].AddScore(10);
            }
        }
    }
}

}