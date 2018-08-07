using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour {

	// Use this for initialization
	void Start()
    {
        print(PlayerPrefsManager.GetPlayerState(1));
        print(PlayerPrefsManager.GetPlayerState(2));
        print(PlayerPrefsManager.GetDifficulty());
        print(PlayerPrefsManager.GetWinningScore());
    }
}
