using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour
{
    Text _text;
    float time = 3;
    void Start()
    {
        _text = GetComponent<Text>();
        _text.enabled = false;
    }

    void Update()
    {
        if (StageManager.instance.stageState == "end")
        {
            if (!_text.enabled)
            {
                _text.enabled = true;
                var winner = StageManager.instance.winner;
                if (winner > 0)
                    _text.text = $"PLAYER {winner} WINS";
                else
                    _text.text = "NO ONE WINS";
            }

        }
        
    }
}
