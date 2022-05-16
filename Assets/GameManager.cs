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
    [Header("Add stage names here in AssignControls scene")]
    public List<string> stages;
    int currentStageIndex = 0;
    List<int> assignedControllers;
    public int numberOfPlayers = 4; // not implemented completely yet
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
        if (StageManager.instance == null)
            return;
        if (StageManager.instance.stageState == StageManager.StageState.SetControllers)
        {
            if (assignedControllers.Count < players.Count)
                DetectControllers();

            if (Input.GetButtonDown("Start"))
            {
                SceneManager.LoadScene(stages[currentStageIndex]);
            }
        }
        else if (StageManager.instance.stageState == StageManager.StageState.Pause)
        {
            if (Input.GetButtonDown("Select"))
                SceneManager.LoadScene("AssignControls");
            if (Input.GetButtonDown("L1") && Input.GetButtonDown("R1"))
                Application.Quit();

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


    int ParseJoyNum() // https://answers.unity.com/questions/1039087/how-to-assign-a-determined-joystick-number-to-each.html
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
