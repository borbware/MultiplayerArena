using UnityEngine;
using UnityEngine.UI;

public class ReadyText : MonoBehaviour
{
    Text _text;
    float time = 3;
    void Start()
    {
        _text = GetComponent<Text>();
        if (StageManager.instance == null)
            _text.enabled = false;
    }

    void Update()
    {
        if (StageManager.instance != null
        && StageManager.instance.stageState == StageManager.StageState.Ready)
        {
            time -= Time.deltaTime;
            if (time > 2)
                _text.text = "3";
            else if (time > 1)
                _text.text = "2";
            else if (time > 0)
                _text.text = "1";
            else
            {
                _text.text = "GO!";
                Invoke("HideText",1.0f);
                StageManager.instance.StartPlay();
            }
        }
    }

    void HideText()
    {
        _text.enabled = false;
    }
}
