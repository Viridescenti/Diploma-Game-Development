using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy
{
    public class Player : MonoBehaviour, IReset
    {
        #region Variables
        [SerializeField] private float _jumpPower;
        private Rigidbody2D _rb;
        // Manages the round
        private RoundManager _roundManager; 
        // Starting position of player
        private Vector3 _homePosition;
        private ScoreKeeper _scoreKeeper;
        private Rigidbody2D _currentPipe;
        private bool _canScore;
        private UIManager _uiManager;
        #endregion

        #region Start
        void Start()
        {
            // Gets component and saves to RB
            _rb = GetComponent<Rigidbody2D>();
            _scoreKeeper = GetComponent<ScoreKeeper>();
            _roundManager = FindObjectOfType<RoundManager>();
            _homePosition = transform.position;
            //
            _uiManager = FindObjectOfType<UIManager>();
        }
        #endregion

        #region Update
        void Update()
        {
            #region Score Logic
            //If we click the left mouse button...
            if (Input.GetMouseButtonDown(0))
            {
                // Set our rb velocity to uppies
                _rb.velocity = Vector2.up * _jumpPower;
            }

            // raycast to check if there's a pipe around us
            RaycastHit2D hit = Physics2D.Raycast(transform.position +
                Vector3.up * 0.5f, Vector2.up);

            // If there is, get prepared to score
            if (hit)
            {
                //if (_currentPipe == null)
                if (!_currentPipe) 
                {
                    _canScore = true;
                    _currentPipe = hit.rigidbody;
                }
            }
            else
            {
                _currentPipe = null;
            }

            // If we meet scoring conditions, increase the score
            if (_currentPipe && _canScore && transform.position.x >
                _currentPipe.position.x)
            {
                _canScore = false;
                _scoreKeeper.IncreaseScore(1);
                _uiManager.UpdateCurrentScore(_scoreKeeper.Score);
            }
            #endregion
        }
        #endregion

        #region Behaviours
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Round ends after colliding with pipes hitbox
            _rb.simulated = false;
            _roundManager.RoundStop();
            _uiManager.UpdateHighScore(_scoreKeeper.TryToSaveHighscore("FlappyBird") ? _scoreKeeper.Score :
                _scoreKeeper.GetHighscore("FlappyBird"));
        }
        #endregion

        #region Reset
        public void Reset()
        {
            // Pipes to starting position
            _rb.simulated = true;
            transform.position = _homePosition;
        }
        #endregion
    }
}
