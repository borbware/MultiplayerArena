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

        _continueText = transform.Find("ContinueText").gameObject.GetComponent<Text>();
        _continueText.enabled = false;
    }

    void Update()
    {
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
        }
        
    }
}
