using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy
{
    public class PipePair : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        private Rigidbody2D _rb;

        // Start is called before the first frame update
        void Start()
        {
            Go();
        }

        public void Go()
        {
            if (_rb == null)
            {
                _rb = GetComponent<Rigidbody2D>();
            }
            _rb.velocity = Vector2.left * _speed;
        }

        public void Stop()
        {
            _rb.velocity = Vector2.zero;
        }
    }
}
