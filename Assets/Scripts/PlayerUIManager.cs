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
    void Start()
    {
        hpBar = transform.Find("HP/Green").gameObject;
        hpBarRT = hpBar.GetComponent<RectTransform>();

        scoreGameObj = transform.Find("Score").gameObject;
        scoreText = scoreGameObj.GetComponent<Text>();
    }
    public void addHP(float newHP)
    {
        hp += newHP;
        hp = Mathf.Clamp(hp, 0, 100);
        if (hp == 0)
            StageManager.instance.LosePlayer(player);
        hpBarRT.sizeDelta = new Vector2(60 * hp / 100, 11);
    }
    public void addScore(int newScore)
    {
        score += newScore;
        score = Mathf.Clamp(score, 0, 20);
        scoreText.text = newScore.ToString();
    }

}
