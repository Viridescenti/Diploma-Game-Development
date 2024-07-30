using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementV2 : MonoBehaviour
{ 
    private Rigidbody2D rb;

    public Vector3 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sideInput = new Vector3(Input.GetAxis("Horizontal"),0,0);

        input = sideInput;
    }

    private void FixedUpdate()
    {
        rb.velocity = input + Vector3.up * rb.velocity.y;
    }
}
