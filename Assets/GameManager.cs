using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text winnerText;
    [SerializeField] Image fadeImage;
    
    ScoreKeeper scoreKeeper;
    Paddle[] paddles;
    Ball ball;
    Paddle winner;

    void Awake()
    {
        scoreKeeper = GetComponent<ScoreKeeper>();
        paddles = FindObjectsOfType<Paddle>();
        ball = FindObjectOfType<Ball>();
    }

    void Start()
    {
        StartGame();
    }
	
	void Update()
    {
		if (scoreKeeper.IsGameOver())
        {
            EndGame();
            DisplayWinner();
        }
	}

    void StartGame()
    {
        winnerText.text = "";
        winnerText.gameObject.SetActive(false);
    }

    void EndGame()
    {
        paddles[0].SetControls(false);
        paddles[1].SetControls(false);
        ball.gameObject.SetActive(false);
        fadeImage.gameObject.SetActive(true);
        winnerText.gameObject.SetActive(true);
    }

    void DisplayWinner()
    {
        winnerText.text = "The Winner is...\n\n";

        Paddle winningPaddle = scoreKeeper.GetWinner();
        string paddleName = winningPaddle.GetName();

        winnerText.text += paddleName + "!";
    }
}
