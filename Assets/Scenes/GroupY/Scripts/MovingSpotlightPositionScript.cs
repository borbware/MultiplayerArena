using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpotlightPositionScript : MonoBehaviour
{

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

    // OnTriggerStay() Käytä tätä
}
