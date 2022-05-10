using System.Collections.Generic;
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

    public float stageTime;
    public List<PlayerUIManager> UIManagers;
    public static StageManager instance;
    public List<int> activePlayers;
    public StageState stageState = StageState.Ready;
    public WinCond winCondition = WinCond.MostHP;
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
            int playerWithMostHP = 0;
            float maxHP = 0;
            foreach (var manager in UIManagers)
            {
                Debug.Log(manager.hp + " " + maxHP);
                playerWithMostHP = (manager.hp > maxHP) ? manager.player : playerWithMostHP;
            }
            return playerWithMostHP;
        }
        int MostScore()
        {
            int playerWithMostScore = 0;
            float maxScore = 0;
            foreach (var manager in UIManagers)
            {
                playerWithMostScore = (manager.score > maxScore) ? manager.player : playerWithMostScore;
            }
            return playerWithMostScore;
        }

        stageState = StageState.End;
        if (winCondition == WinCond.MostHP)
            WinPlayer(MostHP());
        else if (winCondition == WinCond.MostScore)
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
