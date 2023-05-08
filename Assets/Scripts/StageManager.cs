using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{

	public enum StageState
	{
		Ready,
		Play,
		End,
        Pause,
        SetControllers,
	};
    public enum WinCond
    {
        NoWinCond,
        MostHP,
        MostScore
    }

    public static StageManager instance;
    [Header("Stage setup")]
    public float stageTime;
    public WinCond timerZeroWinCondition = WinCond.MostHP;
    public bool loseWhenHPZero = true;
    public bool loseWhenScoreZero = false;

    [Header("UI elements")]
    public bool showHP = true;
    public bool showScore = true;
    public bool showTimer = true;
    [Header("Do not touch")]
    public StageState stageState = StageState.Ready;
    public List<PlayerUIManager> UIManagers;
    public List<GameObject> playerGameObjects;
    public int winner = 0;
    List<int> activePlayers;
    void Awake()
    {
        instance = this;
        activePlayers = new List<int>();
    }
    void Start()
    {
        for (int i = 0; i < GameManager.instance.numberOfPlayers; i++)
        {
            activePlayers.Add(i + 1);
        }
        for (int i = UIManagers.Count - 1; i >= 0; i--)
        {
            if (UIManagers.Count > GameManager.instance.numberOfPlayers)
            {
                Destroy(UIManagers[i].gameObject);
                UIManagers.RemoveAt(i);
                Destroy(playerGameObjects[i]);
                playerGameObjects.RemoveAt(i);
            }
        }
    }
    void Update()
    {
        if (stageState == StageState.Play)
        {
            stageTime -= Time.deltaTime;
            if (stageTime <= 0)
            {
                stageTime = 0;
                TimeUp();
            }
            if (Input.GetButtonDown("Start"))
            {
                stageState = StageState.Pause;
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
        } else if (stageState == StageState.Pause)
        {
            if (Input.GetButtonDown("Start"))
            {
                stageState = StageState.Play;
                Time.timeScale = 1;
                AudioListener.pause = false;
            }
        }
    }
    public void StartPlay()
    {
        stageState = StageState.Play;
    }
    public void AddScore(int player, int newScore)
    {
        UIManagers[player - 1].AddScore(newScore);
    }
    public void AddHP(int player, int newHP)
    {
        UIManagers[player - 1].AddHP(newHP);
    }
    public void LosePlayer(int player)
    {
        activePlayers.Remove(player);
        Destroy(playerGameObjects[player - 1], 1f);
        if (activePlayers.Count == 1)
            WinPlayer(activePlayers[0]);
    }
    public void WinPlayer(int player)
    {
        // player can be 0 if no one wins
        winner = player;
        stageState = StageState.End;
        GameManager.instance.WinPlayer(player);
    }
    public void TimeUp()
    {
        int MostHP()
        {
            var topPlayers = UIManagers
                .Where(p => activePlayers.Contains(p.player))
                .GroupBy(p => p.hp)
                .OrderByDescending(g => g.Key)
                .FirstOrDefault();
            if (topPlayers?.Count() == 1)
                return topPlayers.First().player;
            return -1; // tie
        }

        int MostScore()
        {
            var topPlayers = UIManagers
                .Where(p => activePlayers.Contains(p.player))
                .GroupBy(p => p.score)
                .OrderByDescending(g => g.Key)
                .FirstOrDefault();
            if (topPlayers?.Count() == 1)
                return topPlayers.First().player;
            return -1; // tie
        }

        stageState = StageState.End;
        if (timerZeroWinCondition == WinCond.MostHP)
            WinPlayer(MostHP());
        else if (timerZeroWinCondition == WinCond.MostScore)
            WinPlayer(MostScore());
        else
            WinPlayer(0);

    }
    public void ShowWins()
    {
        foreach (var manager in UIManagers)
        {
            manager.ShowWins();
        }
    }
}
