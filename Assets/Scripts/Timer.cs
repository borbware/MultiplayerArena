using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float time = 120;
    Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
        SetTimerText(time);
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
        if (StageManager.instance.stageState == "play")
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
                StageManager.instance.TimeUp();
            }
            SetTimerText(time);
        }
    }
}
