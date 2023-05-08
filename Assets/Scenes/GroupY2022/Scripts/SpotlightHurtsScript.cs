using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightHurtsScript : MonoBehaviour
{

    [SerializeField] float damage = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        var obj = other.gameObject;
        {
            obj.SendMessage("Hurt", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
