using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] List<PlayerUIManager> UIManagers;
    public static StageManager instance;
    public List<int> activePlayers;
    public string stageState = "ready"; // ready, play, end
    public string winCondition = "mostScore"; // "mostHP",
    public int winner = 0;

    void Start()
    {
        activePlayers = new List<int> {1, 2, 3, 4};
        instance = this;
    }

    public void AddScore(int player, int newScore)
    {
        UIManagers[player - 1].addScore(newScore);
    }
    public void AddHP(int player, int newHP)
    {
        UIManagers[player - 1].addHP(newHP);
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
        stageState = "end";
        // GameManager.instance.wins[player] += 1;
    }


    public void TimeUp()
    {
        stageState = "end";
        if (winCondition == "MostHP")
            WinPlayer(MostHP());
        else if (winCondition == "MostScore")
            WinPlayer(MostScore());

    }
    public int MostHP()
    {
        int playerWithMostHP = 0;
        float maxHP = 0;
        foreach (var manager in UIManagers)
        {
            playerWithMostHP = (manager.hp > maxHP) ? manager.player : playerWithMostHP;
        }
        return playerWithMostHP;
    }
    public int MostScore()
    {
        int playerWithMostScore = 0;
        float maxScore = 0;
        foreach (var manager in UIManagers)
        {
            playerWithMostScore = (manager.score > maxScore) ? manager.player : playerWithMostScore;
        }
        return playerWithMostScore;
    }

    public void StartPlay()
    {
        stageState = "play";
    }
}
