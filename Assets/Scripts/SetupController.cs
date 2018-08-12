using UnityEngine;

public class SetupController : MonoBehaviour
{
	void Start ()
    {
        // Setup initial PlayerPrefs if none have been set
        if (PlayerPrefsManager.GetPlayerState(1) == 0)
            PlayerPrefsManager.SetPlayerState(1, 1);    // make Player 1 human
        if (PlayerPrefsManager.GetDifficulty() == 0)
            PlayerPrefsManager.SetDifficulty(2);        // make difficulty medium
        if (PlayerPrefsManager.GetWinningScore() == 0)
            PlayerPrefsManager.SetWinningScore(11);     // set winning score to 11
    }
}
