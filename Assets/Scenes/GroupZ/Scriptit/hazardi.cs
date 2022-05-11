using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardi : MonoBehaviour
{
    float time, t;
    [SerializeField] bool lerping, activated;
    // Start is called before the first frame update
    void Start()
    {
        time = StageManager.instance.stageTime;
        lerping = false;
        activated = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time < (StageManager.instance.stageTime/2) && activated != true) {
            activated = true;
            lerping = true;
            t = 0;
        }
        if (lerping) {
            transform.localPosition = Vector3.Lerp(new Vector3(2.5f, -7.5f, 2.2f), new Vector3(2.5f, -1, 2.2f), t);
            t += Time.deltaTime;
            if (transform.localPosition == new Vector3(2.5f, -1, 2.2f)) 
                lerping = false;
        }
        if (!lerping && activated) {
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(0).GetComponent<Collider>().enabled = true;
            transform.Rotate(Vector3.Lerp(new Vector3(0,0,0),new Vector3(0,180,0), t/120), Space.Self);
        }
        time -= Time.deltaTime;
    }
}
