using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsData : MonoBehaviour
{
    #region Variables
    private Rigidbody _rb;
    private Joint _joint;

    #endregion
    #region Start
    // Start is called before the first frame update
    void Start()
    {
        _joint = GetComponent<Joint>();
        _rb = new Rigidbody();

        Joint[] joints = GetComponentsInChildren<Joint>();
    }
    #endregion
    #region Update
    // Update is called once per frame
    void Update()
    {
        _rb.IsSleeping(); //_rb.sleepThreshold
        /*_rb.detectCollisions*/

        Debug.Log("Force: " + _joint.currentForce + " Torquel: " + "_joint.currentTorque");
    }
    #endregion
}
