using System.Collections;
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

        if (StageManager.instance.stageState == StageManager.StageState.Ready)
        {
        }
        else if (StageManager.instance.stageState == StageManager.StageState.Play)
        {
            if (_winText.enabled)
                _winText.enabled = false;
        } else if (StageManager.instance.stageState == StageManager.StageState.Pause)
        {
            if (!_winText.enabled)
            {
                _winText.enabled = true;
                _winText.text = "PAUSE";
                _readyText.enabled = false;
            }
        } else if (StageManager.instance.stageState == StageManager.StageState.End)
        {
            if (!_winText.enabled)
            {
                IEnumerator EndStage()
                {
                    yield return new WaitForSecondsRealtime(2f);
                    _continueText.enabled = true;
                    StageManager.instance.ShowWins();
                }

                Time.timeScale = 0;
                _winText.enabled = true;
                var winner = StageManager.instance.winner;
                if (winner == 0)
                    _winText.text = "NO ONE WINS";
                else if (winner == -1)
                    _winText.text = "TIE";
                else
                    _winText.text = $"PLAYER {winner} WINS";
                StartCoroutine(EndStage());
            }
            if (_continueText.enabled && Input.GetButtonDown("Fire1"))
            {
                GameManager.instance.NextStage();
                Time.timeScale = 1;
            }
        } else if (StageManager.instance.stageState == StageManager.StageState.SetControllers)
        {
            if (!_continueText.enabled)
                _continueText.enabled = true;
        } 
        
    }
}
