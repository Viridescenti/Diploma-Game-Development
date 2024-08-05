using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Interfaces
public interface IStop
{
    public void Stop();
}

public interface IReset
{
    public void Reset();
}
#endregion

public class RoundManager : MonoBehaviour
{
    #region variables
    // Only one that can CHANGE the round
    private static bool _roundActive;
    // Checks when round is active
    public static bool RoundActive => _roundActive;
    private Flappy.Player _player;
    private Flappy.PipeManager _pipeManager;
    #endregion

    #region Start
    private void Start()
    {
        _player = FindObjectOfType<Flappy.Player>();
        _pipeManager = FindObjectOfType<Flappy.PipeManager>();
    }
    #endregion

    #region RoundStop
    public void RoundStop()
    {
        _roundActive = false;
        _pipeManager.Stop();
    }
    #endregion

    #region RoundStart
    public void RoundStart()
    {
        _roundActive = true;
        _player.Reset();
        _pipeManager.Reset();
    }
    #endregion

}
