using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] private float playerSpeed;
    private float horizontalInput;
    private float verticallInput;
    private Vector3 moveDirection;

    [Header("jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;


    [Header("ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private bool isGrounded;


    [Header("refrences")]
    [SerializeField] Transform orientation;
    private Rigidbody rb;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //check move Input
        myInput();

        //check if player grounded
        if (Physics.CheckSphere(groundCheck.position, 0.2f, groundMask)) isGrounded = true;
        else isGrounded = false;

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);

        //change the player drag
        ControllDrag(); 
    }

    private void FixedUpdate()
    {
        //move
        if(isGrounded)
            rb.AddForce(moveDirection.normalized * playerSpeed , ForceMode.Acceleration);

        else
            rb.AddForce(moveDirection.normalized * playerSpeed *(airDrag/(groundDrag*1.6f)), ForceMode.Acceleration);
    }

    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticallInput = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticallInput + orientation.right * horizontalInput;
    }

    private void ControllDrag()
    {
        if (isGrounded)
            rb.drag = groundDrag;

        else 
            rb.drag = airDrag;
    }
}
