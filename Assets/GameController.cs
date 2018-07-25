using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Paddle server;

    [SerializeField] Text leftPaddleScoreText, rightPaddleScoreText;
    [SerializeField] Ball ball;

    int leftPaddleScore, rightPaddleScore;

	// Use this for initialization
	void Start()
    {
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        leftPaddleScoreText.text = leftPaddleScore.ToString();
        rightPaddleScoreText.text = rightPaddleScore.ToString();
    }

    public void AddScore(string side)
    {
        if (side == "left")
        {
            leftPaddleScore += 1;
        }
        else if (side == "right")
        {
            rightPaddleScore += 1;
        }
        UpdateScoreDisplay();
    }
}
