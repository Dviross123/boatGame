using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;

    float horizontalInput;
    float verticalInput;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (verticalInput > 0)
        {
            MoveBoat();
        }

        TurnBoat();
        LimitAngles();
    }

    void MoveBoat()
    {
        rb.AddForce(transform.forward * verticalInput * speed * Time.deltaTime, ForceMode.Acceleration);
    }

    void TurnBoat()
    {
        rb.AddTorque(transform.up * horizontalInput * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    void LimitAngles()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;

        if (currentRotation.x > 180)
        {
            currentRotation.x -= 360;
        }

        if (currentRotation.z > 180)
        {
            currentRotation.z -= 360;
        }

        currentRotation.x = Mathf.Clamp(currentRotation.x, -15f, 15f);
        currentRotation.z = Mathf.Clamp(currentRotation.z, -15f, 15f);

        Quaternion targetRotation = Quaternion.Euler(currentRotation);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 100f * Time.deltaTime));
    }
}
