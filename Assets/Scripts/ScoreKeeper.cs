using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] Text leftPaddleScoreText, rightPaddleScoreText;

    int winningScore;
    GameManager gameManager;
    int leftPaddleScore, rightPaddleScore;

	void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        winningScore = PlayerPrefsManager.GetWinningScore();
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

    public bool IsGameOver()
    {
        return leftPaddleScore == winningScore || rightPaddleScore == winningScore;
    }

    public Paddle GetWinner()
    {
        if (!IsGameOver())
            return null;
        else if (leftPaddleScore > rightPaddleScore)
            return gameManager.paddles[0];
        else
            return gameManager.paddles[1];
    }
}
