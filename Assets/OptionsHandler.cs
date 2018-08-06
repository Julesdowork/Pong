using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    public static OptionsHandler instance;

    [SerializeField] Text player1StateText;
    [SerializeField] Text player2StateText;
    [SerializeField] Slider winningScoreSlider;
    [SerializeField] Text winningScoreText;
    //[SerializeField] Text particlesText;
    [SerializeField] Button easyButton;
    [SerializeField] Button mediumButton;
    [SerializeField] Button hardButton;

    enum PaddleState { Player, Computer };
    enum Difficulty { Easy, Medium, Hard};

    PaddleState player1 = PaddleState.Player;
    PaddleState player2 = PaddleState.Computer;
    Difficulty difficultyLevel = Difficulty.Medium;
    int winningScore = 11;
    //bool particlesOn = true;
    Color deselectedColor = new Color(1f, 1f, 1f, 0.478f);
    Color selectedColor = new Color(1f, 1f, 1f, 1f);
    ColorBlock cb;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        cb = easyButton.colors;
    }

    public void ChangePlayer1State()
    {
        if (player1 == PaddleState.Player)
        {
            player1 = PaddleState.Computer;
            player1StateText.text = "Computer";
        }
        else
        {
            player1 = PaddleState.Player;
            player1StateText.text = "Player";
        }
    }

    public void ChangePlayer2State()
    {
        if (player2 == PaddleState.Player)
        {
            player2 = PaddleState.Computer;
            player2StateText.text = "Computer";
        }
        else
        {
            player2 = PaddleState.Player;
            player2StateText.text = "Player";
        }
    }

    public void SetDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "easy": difficultyLevel = Difficulty.Easy;
                break;
            case "medium": difficultyLevel = Difficulty.Medium;
                break;
            case "hard": difficultyLevel = Difficulty.Hard;
                break;
            default: Debug.LogError("Problem setting the difficulty level.");
                break;
        }
        ChangeDifficultyButtonState();
    }

    void ChangeDifficultyButtonState()
    {
        easyButton.colors = cb;
        mediumButton.colors = cb;
        hardButton.colors = cb;
        
        cb.highlightedColor = selectedColor;

        switch (difficultyLevel)
        {
            case Difficulty.Easy: easyButton.colors = cb;
                break;
            case Difficulty.Medium: mediumButton.colors = cb;
                break;
            case Difficulty.Hard: hardButton.colors = cb;
                break;
            default: Debug.LogError("There was a problem setting the colors for the difficulty buttons.");
                break;
        }
        
        cb.highlightedColor = deselectedColor;
    }

    public void SetWinningScore()
    {
        winningScore = (int)winningScoreSlider.value;
        winningScoreText.text = winningScore.ToString();
    }

    // Todo: make particle effect when ball strikes paddles
    /*public void ParticlesSwitch()
    {
        particlesOn = !particlesOn;

        if (particlesOn)
            particlesText.text = "On";
        else
            particlesText.text = "Off";
    }*/
}
