using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int[] wins;
    public List<string> stages;
    public int currentStageIndex = 0;
    void Awake()
    {
        wins = new int[4] {0, 0, 0, 0};
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
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
            wins[player - 1] += 1;
    }
}
