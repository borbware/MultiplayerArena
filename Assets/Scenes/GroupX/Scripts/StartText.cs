using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{

public Text SText;


    // Start is called before the first frame update
    void Start()
    {
        SText.gameObject.SetActive(true);
     //   SText.text = "Press B to jump & Press A to shoot";
        Invoke("HideTitle", 2f);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

        void HideTitle()
    {
        SText.gameObject.SetActive(false);
    }
}
