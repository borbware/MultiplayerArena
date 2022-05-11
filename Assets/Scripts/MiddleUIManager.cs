using UnityEngine;
using UnityEngine.UI;

public class MiddleUIManager : MonoBehaviour
{
    Text _winText, _continueText, _readyText;
    public float _winTime;
    void Start()
    {
        _winText = transform.Find("WinText").gameObject.GetComponent<Text>();
        _winText.enabled = false;

        _continueText = transform.Find("ContinueText").gameObject.GetComponent<Text>();
        _continueText.enabled = false;

        _readyText = transform.Find("ReadyText").gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (StageManager.instance == null)
            return;
        if (StageManager.instance.stageState == StageManager.StageState.End)
        {
            if (!_winText.enabled)
            {
                _winText.enabled = true;
                _winTime = Time.time;
                var winner = StageManager.instance.winner;
                if (winner > 0)
                    _winText.text = $"PLAYER {winner} WINS";
                else
                    _winText.text = "NO ONE WINS";
            }
            if (Time.time > _winTime + 3f)
            {
                if (!_continueText.enabled)
                {
                    _continueText.enabled = true;
                    StageManager.instance.ShowWins();
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    GameManager.instance.NextStage();
                }
            }
        } else if (StageManager.instance.stageState == StageManager.StageState.SetControllers)
        {
            if (!_continueText.enabled)
                _continueText.enabled = true;
        } else if (StageManager.instance.stageState == StageManager.StageState.Pause)
        {
            if (!_winText.enabled)
            {
                _winText.enabled = true;
                _winText.text = "PAUSE";
                _readyText.enabled = false;
            }
        } else if (StageManager.instance.stageState == StageManager.StageState.Play)
        {
            if (_winText.enabled)
                _winText.enabled = false;
        }
        
    }
}
