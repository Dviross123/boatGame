using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class playerManager : MonoBehaviour
{
    [SerializeField] playerSwim playerSwim;
    [SerializeField] Floater floater;
    [SerializeField] PlayerController playerController;


    [SerializeField] WaterSurface water;
    WaterSearchParameters Search;
    WaterSearchResult SearchResult;

    bool inWater;
    [SerializeField] Animator animator;

    public Transform groundCheck;
    public LayerMask groundMask;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Search.startPositionWS = transform.position;

        water.ProjectPointOnWaterSurface(Search, out SearchResult);

        inWater = transform.position.y < SearchResult.projectedPositionWS.y;


        if (inWater)
        {
            animator.SetBool("inWater", true);
            playerSwim.enabled = true;
            floater.enabled = true;
            playerController.enabled = false;
        }
        else 
        {
            animator.SetBool("inWater", false);
            animator.SetBool("isSwim", false);
            playerSwim.enabled = false;
            floater.enabled = false;
            playerController.enabled = true;
        }

    }

    public bool isGrounded()
    {
        if (Physics.CheckSphere(groundCheck.position, 0.2f, groundMask)) return true;
        return false;
    }




}
