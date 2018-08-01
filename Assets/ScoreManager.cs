using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public bool isGameOver;

    [SerializeField] Text leftPaddleScoreText, rightPaddleScoreText;

    int leftPaddleScore, rightPaddleScore;

	// Use this for initialization
	void Start()
    {
        UpdateScoreDisplay();
    }

    void Update()
    {
        if (leftPaddleScore == 2 || rightPaddleScore == 2)
        {
            print("Game Over!");
            isGameOver = true;
        }
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
