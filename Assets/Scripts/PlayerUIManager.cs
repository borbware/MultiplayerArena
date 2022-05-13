using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public int player; 
    public float hp = 100;
    public int score = 0;
    GameObject hpBarObj;
    RectTransform hpBarRT;

    GameObject scoreObj;
    Text scoreText;

    GameObject winCountObj;
    Text winCountText;
    float width;
    void Awake()
    {
        hpBarObj = transform.Find("HP/bar").gameObject;
        hpBarRT = hpBarObj.GetComponent<RectTransform>();
        width = hpBarRT.rect.width;

        scoreObj = transform.Find("Score").gameObject;
        scoreText = scoreObj.GetComponent<Text>();

        winCountObj = transform.Find("WinCount").gameObject;
        winCountText = winCountObj.GetComponent<Text>();

    }
    void Start() {

        HideWins();
        AddHP(0);
        AddScore(0);

        if (!StageManager.instance.showHP)
        {
            var hpObj = transform.Find("HP").gameObject;
            hpObj.SetActive(false);
            scoreObj.transform.position += new Vector3(0,10,0);
        }
        if (!StageManager.instance.showScore)
        {
            scoreObj.SetActive(false);
        }
    }
    public void AddHP(float newHP)
    {
        hp += newHP;
        hp = Mathf.Clamp(hp, 0, 100);
        hpBarRT.sizeDelta = new Vector2(width * hp / 100, hpBarRT.rect.height);
        if (StageManager.instance.loseWhenHPZero && hp == 0)
            StageManager.instance.LosePlayer(player);
    }
    public void AddScore(int newScore)
    {
        score += newScore;
        score = Mathf.Clamp(score, 0, 20);
        scoreText.text = score.ToString();
        if (StageManager.instance.loseWhenScoreZero && score == 0)
            StageManager.instance.LosePlayer(player);
    }
    public void ShowWins()
    {
        var wins = GameManager.instance.players[player - 1].wins;
        winCountText.text = $"{wins} WINS";
        winCountText.enabled = true;
    }
    public void HideWins()
    {
        winCountText.enabled = false;
    }
}
