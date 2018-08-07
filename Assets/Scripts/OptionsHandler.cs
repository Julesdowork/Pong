using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    [SerializeField] Text player1StateText;
    [SerializeField] Text player2StateText;
    [SerializeField] Slider winningScoreSlider;
    [SerializeField] Text winningScoreText;
    //[SerializeField] Text particlesText;
    [SerializeField] Button easyButton;
    [SerializeField] Button mediumButton;
    [SerializeField] Button hardButton;
    
    //bool particlesOn = true;
    Color deselectedColor = new Color(1f, 1f, 1f, 0.478f);
    Color selectedColor = new Color(1f, 1f, 1f, 1f);
    ColorBlock cb;

    void Start()
    {
        cb = easyButton.colors;
        DisplayWinningScore();
        DisplayDifficultyState();
        DisplayPlayerStates();
    }

    public void ChangePlayer1State()
    {
        if (PlayerPrefsManager.GetPlayerState(1) == 1)
            PlayerPrefsManager.SetPlayerState(1, 0);    // 0 = Computer
        else
            PlayerPrefsManager.SetPlayerState(1, 1);    // 1 = Player

        DisplayPlayerStates();
    }

    public void ChangePlayer2State()
    {
        if (PlayerPrefsManager.GetPlayerState(2) == 1)
            PlayerPrefsManager.SetPlayerState(2, 0);
        else
            PlayerPrefsManager.SetPlayerState(2, 1);

        DisplayPlayerStates();
    }

    void DisplayPlayerStates()
    {
        if (PlayerPrefsManager.GetPlayerState(1) == 0)
            player1StateText.text = "Computer";
        else
            player1StateText.text = "Player";
        if (PlayerPrefsManager.GetPlayerState(2) == 0)
            player1StateText.text = "Computer";
        else
            player1StateText.text = "Player";
    }

    public void ChangeDifficulty(int difficulty)
    {
        PlayerPrefsManager.SetDifficulty(difficulty);
        DisplayDifficultyState();
    }

    void DisplayDifficultyState()
    {
        easyButton.colors = cb;
        mediumButton.colors = cb;
        hardButton.colors = cb;

        cb.normalColor = selectedColor;
        cb.highlightedColor = selectedColor;

        int difficultyLevel = PlayerPrefsManager.GetDifficulty();
        switch (difficultyLevel)
        {
            case 1: easyButton.colors = cb;
                break;
            case 2: mediumButton.colors = cb;
                break;
            case 3: hardButton.colors = cb;
                break;
            default: Debug.LogError("There was a problem setting the colors for the difficulty buttons.");
                break;
        }
        
        cb.highlightedColor = deselectedColor;
    }

    public void SetWinningScore()
    {
        PlayerPrefsManager.SetWinningScore((int)winningScoreSlider.value);
        DisplayWinningScore();
    }

    void DisplayWinningScore()
    {
        winningScoreText.text = winningScoreSlider.value.ToString();
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
