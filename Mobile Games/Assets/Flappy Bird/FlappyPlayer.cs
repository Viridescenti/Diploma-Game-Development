using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy
{
    public class FlappyPlayer : MonoBehaviour
    {
        [SerializeField] private float _jumpPower;
        private Rigidbody2D _rb;

        // Managers the round
        private RoundManager _roundManager;
        
        // Starting position of player
        private Vector3 _homePosition;

        void Start()
        {
            // Gets component and saves to RB
            _rb = GetComponent<Rigidbody2D>();
            _roundManager = FindObjectOfType<RoundManager>();
            _homePosition = transform.position;
        }

        void Update()
        {
            //If we click the left mouse button...
            if (Input.GetMouseButtonDown(0))
            {
                // Set our rb velocity to uppies
                _rb.velocity = Vector2.up * _jumpPower;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Round ends after colliding with pipes hitbox
            _rb.simulated = false;
            _roundManager.RoundStop();
        }

        public void Reset()
        {
            // Pipes to starting position
            _rb.simulated = true;
            transform.position = _homePosition;
        }
    }
}
