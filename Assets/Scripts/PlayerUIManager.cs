using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public int player; 
    public float hp = 100;
    public int score = 0;
    GameObject hpBar;
    RectTransform hpBarRT;

    GameObject scoreGameObj;
    Text scoreText;

    GameObject winCountObj;
    Text winCountText;
    void Awake()
    {
        hpBar = transform.Find("HP/bar").gameObject;
        hpBarRT = hpBar.GetComponent<RectTransform>();

        scoreGameObj = transform.Find("Score").gameObject;
        scoreText = scoreGameObj.GetComponent<Text>();

        winCountObj = transform.Find("WinCount").gameObject;
        winCountText = winCountObj.GetComponent<Text>();
        HideWins();
    }
    public void AddHP(float newHP)
    {
        hp += newHP;
        hp = Mathf.Clamp(hp, 0, 100);
        if (hp == 0)
            StageManager.instance.LosePlayer(player);
        hpBarRT.sizeDelta = new Vector2(hpBarRT.rect.width * hp / 100, hpBarRT.rect.height);
    }
    public void AddScore(int newScore)
    {
        score += newScore;
        score = Mathf.Clamp(score, 0, 20);
        scoreText.text = newScore.ToString();
    }
    public void ShowWins()
    {
        Debug.Log("sadf");
        var wins = GameManager.instance.players[player - 1].wins;
        winCountText.text = $"{wins} WINS";
        winCountText.enabled = true;
    }
    public void HideWins()
    {
        winCountText.enabled = false;
    }
}
