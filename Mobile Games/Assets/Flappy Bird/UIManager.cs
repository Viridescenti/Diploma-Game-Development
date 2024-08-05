using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IReset, IStop
{
    #region Variables
    [SerializeField] private GameObject _endPanel;

    [SerializeField] private TextMeshProUGUI _labelCurrentScore, _labelHighScore;
    #endregion

    #region Updating Current/HighScore
    public void UpdateCurrentScore(int score)
    {
        _labelCurrentScore.text = score.ToString();

    }


    public void UpdateHighScore(int score)
    {
        _labelHighScore.text = "Highscore: " + score.ToString();   
    }
    #endregion

    #region Stop
    public void Stop()
    {
        _endPanel.SetActive(true);
    }
    #endregion

    #region Reset
    public void Reset()
    {
        _endPanel.SetActive(false);
        UpdateCurrentScore(0);
    }
    #endregion
}
