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
    public List<int> activePlayers;
    public int winner = 0;

    void Awake()
    {
        activePlayers = new List<int> {1, 2, 3, 4};
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            UIManagers[i] = GameObject.Find($"Player{i + 1}UI").GetComponent<PlayerUIManager>();
        }
    }
    void Update()
    {
        if (stageState == StageState.Play)
        {
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
            return UIManagers.OrderByDescending(manager => manager.hp).FirstOrDefault()?.player ?? 0;
        }

        int MostScore()
        {
            return UIManagers.OrderByDescending(manager => manager.score).FirstOrDefault()?.player ?? 0;
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
