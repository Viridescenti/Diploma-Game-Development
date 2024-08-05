using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour, IReset
{
    private int _score;
    public int Score => _score;


    public void IncreaseScore(int points)
    {
        _score += points;
    }

    public void Reset()
    {
        _score = 0;
    }

    public bool TryToSaveHighscore(string gameName)
    {
        // If the game has not highscore data, OR our score is larger than the highscore
        if (_score > GetHighscore(gameName))
        {
            PlayerPrefs.SetInt(gameName, _score);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public int GetHighscore(string gameName)
    {
        // This is identical to the code below...
        return PlayerPrefs.HasKey(gameName) ? PlayerPrefs.GetInt(gameName) : 0;


        // Check if a highscore exists
        if (!PlayerPrefs.HasKey(gameName))
            // If not, return 0
            return 0;
        // Else, we return that score
        return PlayerPrefs.GetInt(gameName);
    }
}
