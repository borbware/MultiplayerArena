using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole_script : MonoBehaviour
{   
    [SerializeField] public float mole_lifetime = 5f;
    public int i_am_in_hole_no = 0;

    private void OnDestroy() {
        // we set call the mole_spawner script to set the hole the mole was in to empty
        GameObject.Find("RunningScripts").GetComponent<mole_spawner>()
        .array_of_holes[i_am_in_hole_no].set_empty();
        //Debug.Log($"hole no {i_am_in_hole_no} is empty");
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
