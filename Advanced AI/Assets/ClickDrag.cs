using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDrag : MonoBehaviour
{
    #region Variables
    public float forceAmount = 500f;
    public float reach = 162f;
    public float maxForce = 13f;
    public float yeetForce = 420f;
    Rigidbody _dragObject;
    float _selectionDistance;
    Camera _camera;
    #endregion


    #region Start
    //public GameObject marker;
    private void Start()
    {
        _camera = Camera.main ? Camera.main : FindObjectOfType<Camera>();
    }
    #endregion

    #region Update
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        #region Mouse Button 0
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            // Bool value
            if (Physics.Raycast(ray, out hit, reach))
            {
                _selectionDistance = 1f; //hit.distance; 
                _dragObject = hit.rigidbody;
            }
        }
        #endregion

        #region Left MB
        if (Input.GetMouseButtonDown(1))
        {
            _dragObject.AddForce(_camera.transform.forward * yeetForce, ForceMode.Impulse);
            _dragObject = null;
        }
        #endregion


        #region Mouse Button 0
        if (Input.GetMouseButtonDown(0))
        {
            _dragObject = null;
        }

        if (_dragObject != null)
        {
            _selectionDistance += Input.mouseScrollDelta.y * 0.2f;
        }
        #endregion
    }
    #endregion

    #region Fixed Update
    private void FixedUpdate()
    {
        if (_dragObject)
        {
            Vector3 mouseObjectDelta = _camera.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y,
                    _selectionDistance));


            _dragObject.velocity = (mouseObjectDelta - _dragObject.transform.position) 
                * (forceAmount * Time.deltaTime);

            if (_dragObject.velocity.magnitude > maxForce)
            {
                _dragObject.velocity = _dragObject.velocity.normalized * maxForce;
            }
        }
    }
    #endregion

}