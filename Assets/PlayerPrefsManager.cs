using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    const string PLAYER_1_STATE_KEY = "player_1_state";
    const string PLAYER_2_STATE_KEY = "player_2_state";
    const string DIFFICULTY_KEY = "difficulty";
    const string WINNING_SCORE_KEY = "winning_score";

    public static void SetPlayerState(int num, int state)
    {
        if (num == 1)
            PlayerPrefs.SetInt(PLAYER_1_STATE_KEY, state);
        else if (num == 2)
            PlayerPrefs.SetInt(PLAYER_2_STATE_KEY, state);
        else
            Debug.LogError("Could not find the player requested.");
    }

    public static int GetPlayerState(int num)
    {
        if (num == 1)
            return PlayerPrefs.GetInt(PLAYER_1_STATE_KEY);
        else
            return PlayerPrefs.GetInt(PLAYER_2_STATE_KEY);
    }

    public static void SetDifficulty(int num)
    {
        if (num >= 1 && num <= 3)
            PlayerPrefs.SetInt(DIFFICULTY_KEY, num);
        else
            Debug.LogError("Error setting difficulty level. Number given not in range.");
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_KEY);
    }

    public static void SetWinningScore(int num)
    {
        if (num >= 1 && num <= 99)
            PlayerPrefs.SetInt(WINNING_SCORE_KEY, num);
        else
            Debug.Log("Error setting winning score. Number given not in range.");
    }

    public static int GetWinningScore()
    {
        return PlayerPrefs.GetInt(WINNING_SCORE_KEY);
    }
}
