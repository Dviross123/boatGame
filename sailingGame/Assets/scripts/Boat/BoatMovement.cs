using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField] float waterDrag = 0.99f;

    float horizontalInput;
    float verticalInput;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (verticalInput != 0)
        {
            MoveBoat();
        }

        TurnBoat();
        ApplyWaterDrag();
    }

    void MoveBoat()
    {
        Vector3 forceDirection = -transform.forward * verticalInput * speed;
        rb.AddForce(forceDirection, ForceMode.Force);
    }

    void TurnBoat()
    {
        Vector3 turnDirection = transform.up * horizontalInput * turnSpeed;
        rb.AddTorque(turnDirection, ForceMode.Force);
    }

    void ApplyWaterDrag()
    {
        rb.velocity *= waterDrag;
    }
}
