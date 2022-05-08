using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float time = 120;
    Text timerText;
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    void Update()
    {
        if (StageManager.instance.stageState == "play")
        {
            time -= Time.deltaTime;
            var mins = Mathf.Floor( time / 60 );
            var secs = Mathf.Floor( time % 60 );
            timerText.text = mins.ToString() + ":" + secs.ToString();
            if (time <= 0)
            {
                time = 0;
                StageManager.instance.TimeUp();
            }
        }

    }
}
