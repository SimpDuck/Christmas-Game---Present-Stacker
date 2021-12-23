using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    private Score scoreGui;

    private void Awake()
    {
        SetupSingleton();
    }

    private void Start()
    {
        scoreGui = FindObjectOfType<Score>();
    }
    private void SetupSingleton()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if (numberGameSession > 1)
        {
            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }


    public int GetScore()
    {
        return score;
    }


    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        scoreGui.updateScoreText(score);
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
