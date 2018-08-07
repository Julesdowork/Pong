using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Paddle[] paddles = new Paddle[2];

    [SerializeField] Text winnerText;
    [SerializeField] Image fadeImage;
    [SerializeField] Button replayButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject computerPrefab;
    
    ScoreKeeper scoreKeeper;
    Ball ball;
    Paddle winner;

    void Awake()
    {
        print("Left is: " + PlayerPrefsManager.GetPlayerState(1));
        print("Right is: " + PlayerPrefsManager.GetPlayerState(2));
        print("Difficulty set to: " + PlayerPrefsManager.GetDifficulty());
        print("Winning score: " + PlayerPrefsManager.GetWinningScore());
        SetupGame();
        scoreKeeper = GetComponent<ScoreKeeper>();
    }
	
	void Update()
    {
		if (scoreKeeper.IsGameOver())
        {
            EndGame();
            Invoke("ShowEndGameOptions", 5f);
        }
	}

    void SetupGame()
    {
        ActivateWinScreen(false);
        SetupPaddles();
        SetupBall();
    }

    void ActivateWinScreen(bool isActive)
    {
        if (isActive)
        {
            paddles[0].SetControls(false);
            paddles[1].SetControls(false);
            ball.gameObject.SetActive(false);
            fadeImage.gameObject.SetActive(true);
            winnerText.gameObject.SetActive(true);
            DisplayWinner();
        }
        else
        {
            winnerText.text = "";
            winnerText.gameObject.SetActive(false);
            replayButton.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
        }
    }

    void SetupPaddles()
    {
        InstantiatePaddles();
        NamePaddles();
        SetupPaddleDifficulty();
    }

    void InstantiatePaddles()
    {
        GameObject left;
        if (PlayerPrefsManager.GetPlayerState(1) == 1)
        {
            left = Instantiate(playerPrefab, new Vector3(-7f, 0f, 0f), Quaternion.identity) as GameObject;
        }
        else
        {
            left = Instantiate(computerPrefab, new Vector3(-7f, 0f, 0f), Quaternion.identity) as GameObject;
        }
        paddles[0] = left.GetComponent<Paddle>();

        GameObject right;
        if (PlayerPrefsManager.GetPlayerState(2) == 1)
        {
            right = Instantiate(playerPrefab, new Vector3(7f, 0f, 0f), Quaternion.identity) as GameObject;
        }
        else
        {
            right = Instantiate(computerPrefab, new Vector3(7f, 0f, 0f), Quaternion.identity) as GameObject;
        }
        paddles[1] = right.GetComponent<Paddle>();
    }

    void NamePaddles()
    {
        if (paddles[0].isComputer)
            paddles[0].SetName("Computer 1");
        else
            paddles[0].SetName("Player 1");

        if (paddles[1].isComputer)
        {
            if (paddles[0].isComputer)
                paddles[1].SetName("Computer 2");
            else
                paddles[1].SetName("Computer 1");
        }
        else
        {
            if (paddles[0].isComputer)
                paddles[1].SetName("Player 1");
            else
            {
                paddles[1].SetName("Player 2");
                paddles[1].ChangeInputAxis();       // Player 2 should use the arrows to move
            }
        }
    }

    void SetupPaddleDifficulty()
    {
        if (PlayerPrefsManager.GetDifficulty() == 1)
        {
            if (paddles[0].isComputer)
                paddles[0].SetSpeed(3f);
            if (paddles[1].isComputer)
                paddles[1].SetSpeed(3f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 2)
        {
            if (paddles[0].isComputer)
                paddles[0].SetSpeed(6f);
            if (paddles[1].isComputer)
                paddles[1].SetSpeed(6f);
        }
        else
        {
            if (paddles[0].isComputer)
                paddles[0].SetSpeed(8f);
            if (paddles[1].isComputer)
                paddles[1].SetSpeed(8f);
        }
    }

    void SetupBall()
    {
        ball = FindObjectOfType<Ball>();
        if (PlayerPrefsManager.GetDifficulty() == 1)
            ball.SetSpeed(8f);
        else if (PlayerPrefsManager.GetDifficulty() == 2)
            ball.SetSpeed(12f);
        else
            ball.SetSpeed(16f);
    }

    void EndGame()
    {
        ActivateWinScreen(true);
    }

    void DisplayWinner()
    {
        winnerText.text = "The Winner is...\n\n";

        Paddle winningPaddle = scoreKeeper.GetWinner();
        string paddleName = winningPaddle.GetName();

        winnerText.text += paddleName + "!";
    }

    void ShowEndGameOptions()
    {
        replayButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }
}
