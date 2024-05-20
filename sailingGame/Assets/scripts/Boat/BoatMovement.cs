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

    [SerializeField] float fixRotSpeed;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (verticalInput > 0)
        {
            MoveBoat();
        }

        TurnBoat();
      //  LimitAngles();
    }

    void MoveBoat()
    {
        rb.AddForce(transform.forward * verticalInput * speed * Time.deltaTime, ForceMode.Acceleration);
    }

    void TurnBoat()
    {
        rb.AddTorque(transform.up * horizontalInput * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    //void LimitAngles()
    //{
    //    Vector3 currentRotation = transform.rotation.eulerAngles;

    //    if (currentRotation.x > 180)
    //    {
    //        currentRotation.x -= 360;
    //    }

    //    if (currentRotation.z > 180)
    //    {
    //        currentRotation.z -= 360;
    //    }

    //    if (currentRotation.z < -15 || currentRotation.z > 15) 
    //    {
    //        Quaternion temp = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, temp, fixRotSpeed * Time.deltaTime);
    //    }

    //    if (currentRotation.x < -15 || currentRotation.x > 15)
    //    {
    //        Quaternion temp = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, temp, fixRotSpeed * Time.deltaTime);
    //    }
    //}
}
