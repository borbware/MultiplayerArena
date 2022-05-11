using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpotlightPositionScript : MonoBehaviour
{

    [SerializeField] float damage = 2f;
    public Vector3 PositionOfLight;

    // Start is called before the first frame update
    void Start()
    {
        PositionOfLight = new Vector3(0f, 7f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = PositionOfLight;
    }

    private void OnTriggerStay(Collider other) 
    {
        var obj = other.gameObject;
        obj.SendMessage("Hurt", damage, SendMessageOptions.DontRequireReceiver);
    }
}
