using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDrag : MonoBehaviour
{
    public float forceAmount = 500f;

    Rigidbody _dragObject;
    Vector3 _offset;
    vector3 _originalPosition;
    float _selectionDistance;

    //public GameObject marker;
    private void Start()
    {

        bool result =           x == y ? a : b;
        _camera = Camera.main ? Camera.main : FindObjectOfType<Camera>();
    }

    private void update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
