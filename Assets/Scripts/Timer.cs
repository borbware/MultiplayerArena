using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
        if (!StageManager.instance.showTimer)
        {
            _text.enabled = false;
            return;
        }
        SetTimerText(StageManager.instance.stageTime);
    }

    void SetTimerText(float time)
    {
        var mins = Mathf.Floor( time / 60 );
        var secs = Mathf.Floor( time % 60 );
        var secsString = secs < 10 ? "0" + secs.ToString() : secs.ToString();
        _text.text = mins.ToString() + ":" + secsString;
    }

    void Update()
    {
        if (StageManager.instance != null
        && StageManager.instance.stageState == StageManager.StageState.Play)
        {
            SetTimerText(StageManager.instance.stageTime);
        }
    }
}
