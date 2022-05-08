using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float time = 120;
    Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        if (StageManager.instance.stageState == "play")
        {
            time -= Time.deltaTime;
            var mins = Mathf.Floor( time / 60 );
            var secs = Mathf.Floor( time % 60 );
            var secsString = secs < 10 ? "0" + secs.ToString() : secs.ToString();
            _text.text = mins.ToString() + ":" + secsString;
            if (time <= 0)
            {
                time = 0;
                StageManager.instance.TimeUp();
            }
        }

    }
}
