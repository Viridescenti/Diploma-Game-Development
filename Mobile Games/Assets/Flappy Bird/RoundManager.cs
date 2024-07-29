using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    // Only one that can CHANGE the round
    private static bool _roundActive;

    // Checks when round is active
    public static bool RoundActive => _roundActive;

    private Flappy.FlappyPlayer _player;
    private Flappy.PipeManager _pipeManager;

    private void Start()
    {
        _player = FindObjectOfType<Flappy.FlappyPlayer>();
        _pipeManager = FindObjectOfType<Flappy.PipeManager>();
    }

    public void RoundStop()
    {
        _roundActive = false;
        _pipeManager.Stop();
    }

    public void RoundStart()
    {
        _roundActive = true;
        _player.Reset();
        _pipeManager.Reset();
    }

}
