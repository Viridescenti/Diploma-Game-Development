using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDrag : MonoBehaviour
{
    public float forceAmount = 500f;

    Rigidbody _dragObject;
    Vector3 _offset;
    Vector3 _originalPosition;
    float _selectionDistance;

    //public GameObject marker;
    private void Start()
    {
        _camera = Camera.main ? Camera.main : FindObjectOfType<Camera>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            // Bool value
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                _selectionDistance = hit.distance; _dragObject = hit.rigidbody;

                _offset = hit.point;
                    /*_camera.ScreenToViewportPoint(
                    new vector3(Input.mousePosition.x,
                    Input.mousePosition.y,
                    _selectionDistance));*/
                _originalPosition = hit.collider.transform.position;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            _dragObject = null;
        }

        if( _dragObject != null )
        {
            _selectionDistance += Input.mouseScrollDelta.y * 0.2f;
        }
    }

    private void FixedUpdate()
    {
        if(_dragObject)
        {
            Vector3 mouseObjectDelta = _camera.ScreenToViewportPoint(
                    new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y,
                    _selectionDistance));
            _dragObject.velocity = (_originalPosition + mouseObjectDelta
                - _dragObject.transform.position) * forceAmount * Time.deltaTime;
        }
    }

}
