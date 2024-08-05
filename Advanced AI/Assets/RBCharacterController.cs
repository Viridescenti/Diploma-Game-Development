using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBCharacterController : MonoBehaviour
{
    #region Variables
    private Rigidbody _rb;
    private Camera _camera;

    private Vector3 _input;
    #endregion

    #region Start
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>();
    }
    #endregion

    #region Update
    private void Update()
    {
        MovementInput();
        JumpInput();
    }

    #region Jump Input
    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 
            if(_isGrounded || Time.time - coyoteTime < _coyoteTimer) //_coyoteTimer = Time.time
            {
                // If we have jumped...
                _rb.AddForce(Vector3.up * jump, ForceMode.VelocityChange);
                _coyoteTimer = float.NegativeInfinity;
                _isJumping = true;
            }
        }
    }
    #endregion

    #region Movement Logic
    private void MovementInput()
    {
        // Camera
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 _inputTransformed = _camera.transform.TransformDirection(_input);
        _inputTransformed.y = 0f;
        _inputTransformed.Normalize();

        // Ensuring there are no bugs from looking straight up...
        if (_input.sqrMagnitude != 0 && _inputTransformed.sqrMagnitude == 0)
        {
            _input.y = 0f;
            _input.Normalize();
        }
        else
        {
            _input = _inputTransformed;
        }
    }
    #endregion
    #endregion


    #region FixedUpdate
    private void FixedUpdate()
    {
        
    }
    #endregion

    #region CheckGrounded
    private bool CheckGrounded()
    {
        RaycastHit hit; 

         if (Physics.SphereCast(transform.position + offset,
             groundCheckRadius, Vector3.down, out hit, groundCheckDistance))
         {
            //If we're not jumping, we're gonna set CoyoteTime
            if (_isJumping) 
                return false;

            // If not grounded, but last frame was grounded 
            _coyoteTimer = Time.time;

            return true;

         }
        return false;
    }
    #endregion
}
