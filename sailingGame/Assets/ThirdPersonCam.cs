using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField] Transform ori, player, playerObj;
    [SerializeField] Rigidbody rb;

    [SerializeField] float rotationSpeed;

    Vector3 dir;
    Vector3 inputDir;

    void Update()
    {
        // rotate orientation
        dir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        ori.forward = dir.normalized;

        // rotate orientation
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        inputDir = ori.forward * verticalInput + ori.right * horizontalInput;

        if (inputDir != null) 
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
