using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSwim : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField] float waterDrag = 0.99f;
    [SerializeField] Transform orientation;
    Vector3 moveDirection;

    float horizontalInput;
    float verticalInput;

    [SerializeField] Animator animator;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (verticalInput != 0 || horizontalInput != 0)
        {
            MoveBoat();
            animator.SetBool("isSwim", true);
        }
        else
            animator.SetBool("isSwim", false);
    }

    void MoveBoat()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection * speed * Time.deltaTime, ForceMode.Force);
    }

}
