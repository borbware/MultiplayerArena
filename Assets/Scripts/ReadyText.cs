using UnityEngine;
using UnityEngine.UI;

public class ReadyText : MonoBehaviour
{
    Text _text;
    float time = 3;
    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        if (StageManager.instance.stageState == "ready")
        {
            time -= Time.deltaTime;
            if (time > 2)
                _text.text = "READY";
            else if (time > 1)
                _text.text = "SET";
            else if (time > 0)
                _text.text = "GO!";
            else
                StageManager.instance.StartPlay();
        } else {
            _text.enabled = false;
        }
        
    }
}
