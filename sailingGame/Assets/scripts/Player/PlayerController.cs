using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool isMove;

    [SerializeField] endAnimation endAnimation;

    [Header("movement")]
    [SerializeField] private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    [Header("jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;

    [Header("ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [Header("refrences")]
    [SerializeField] Transform orientation;
    private Rigidbody rb;
    [SerializeField] playerManager pm; 

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        isMove = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();

    }

    void Update()
    {
        //check move Input
        MyInput();

        //check if player grounded
        

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && pm.isGrounded())
        {
            rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJump", true);
        }

        //change the player drag
        ControlDrag();

        if(isMove)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        
    }

    private void FixedUpdate()
    {
        //move
        if (pm.isGrounded())
            MoveOnSlope();
        else
            rb.AddForce(moveDirection.normalized * playerSpeed * (airDrag / (groundDrag * 1.6f)), ForceMode.Acceleration);
    }

   

    

    public void stopAnimationCoroutine(float animationTime, string name)
    {
        StartCoroutine(stopAnimation(animationTime, name));
    }

    private IEnumerator stopAnimation(float animationTime, string name)
    {
        yield return new WaitForSeconds(animationTime);
        animator.SetBool(name, false);
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(horizontalInput == 0 && verticalInput == 0)
            isMove = false;
        else
            isMove = true;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
    }

    private void ControlDrag()
    {
        if (pm.isGrounded())
            rb.drag = groundDrag;
        else
            rb.drag = airDrag;
    }

    private void MoveOnSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, 0.3f, groundMask))
        {
            Vector3 slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal).normalized;
            rb.AddForce(slopeMoveDirection * playerSpeed, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * playerSpeed, ForceMode.Acceleration);
        }
    }
}
