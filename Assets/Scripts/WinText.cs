using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour
{
    Text _winText;
    Text _continueText;
    public float _winTime;
    void Start()
    {
        _winText = transform.Find("WinText").gameObject.GetComponent<Text>();
        _winText.enabled = false;

        Transform _continue = transform.Find("ContinueText");
        if (_continue)
        {
            _continueText = _continue.gameObject.GetComponent<Text>();
            _continueText.enabled = false;
        }
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
            if (_continueText && Time.time > _winTime + 3)
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
            _continueText.enabled = true;
        }
        
    }
}
