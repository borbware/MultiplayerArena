using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public class PlayerData
    {
        public int number;
        public int controller;
        public int wins = 0;

        public PlayerData(int _number)
        {
            number = _number;
            controller = number;
        }
    }

    public static GameManager instance;
    public List<PlayerData> players;
    public int numberOfPlayers = 4;
    public List<string> stages;
    public int currentStageIndex = 0;
    public bool enableTimer = true;
    public bool enableHP = true;
    public bool enableScore = true;
    List<int> assignedControllers;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            players = new List<PlayerData>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new PlayerData(i + 1));
            }
            assignedControllers = new List<int>();
        } else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (StageManager.instance.stageState == StageManager.StageState.SetControllers)
        {
            foreach (var player in players)
            {
                player.controller = 0;
            }
        }
    }
    void Update()
    {
        if (StageManager.instance != null && StageManager.instance.stageState == StageManager.StageState.SetControllers)
        {
            if (assignedControllers.Count < players.Count)
                DetectControllers();

            if (Input.GetButtonDown("Start"))
            {
                SceneManager.LoadScene(stages[currentStageIndex]);
            }
        }
    }
    public void NextStage()
    {
        if (stages.Count > 0)
        {
            currentStageIndex = (currentStageIndex + 1) % stages.Count;
            SceneManager.LoadScene(stages[currentStageIndex]);
        } else
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
    public void WinPlayer(int player)
    {
        if (player > 0)
            players[player - 1].wins += 1;
    }


    int ParseJoyNum()
    {
        string joyNumString = "";
        int joyNum = 0;
        for (int action = (int)KeyCode.Backspace; action <= (int)KeyCode.Joystick8Button19; action++)
        {
            if (Input.GetKeyDown((KeyCode)action) && ((KeyCode)action).ToString().Contains("Joystick"))
            {
                joyNumString = ((KeyCode)action).ToString().Substring(8, 2);
                if (joyNumString.EndsWith("B"))
                {
                    joyNumString = joyNumString.Substring(0, 1);
                } else if (joyNumString.StartsWith("B"))
                    continue;
                Debug.Log("This is Joystick Number " + joyNumString);
                Int32.TryParse(joyNumString, out joyNum);
            }
        }
        return joyNum;
    }
    void DetectControllers()
    {
        var joyNum = ParseJoyNum();
        if (joyNum != 0 && !assignedControllers.Contains(joyNum))
        {
            var player = players[assignedControllers.Count];
            player.controller = joyNum;
            assignedControllers.Add(joyNum);
            StageManager.instance.UIManagers[player.number - 1].AddScore(joyNum); // debug
        }
    }
}
