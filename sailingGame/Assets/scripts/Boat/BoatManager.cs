using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BoatManager : MonoBehaviour
{
    [SerializeField] GameObject boatPlayerObj;
    [SerializeField] GameObject realPlayer;
    [SerializeField] BoatMovement boatMovement;
    [SerializeField] CinemachineFreeLook flc;

    bool onBoat;

    public void EnterBoat()
    {
        boatPlayerObj.SetActive(true);
        boatMovement.enabled = true;
        flc.Follow = boatPlayerObj.transform;
        flc.LookAt = boatPlayerObj.transform;
        realPlayer.SetActive(false);
        onBoat = true;
        print("enter boat");
    }

    public void ExitBoat()
    {
        onBoat = false;
        realPlayer.transform.position = transform.position;
        realPlayer.SetActive(true);
        flc.Follow = realPlayer.transform;
        flc.LookAt = realPlayer.transform;
        boatPlayerObj.SetActive(false);
        boatMovement.enabled = false;
        print("exit boat");
    }

    private void Update()
    {
        if (onBoat) 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
                ExitBoat();
        }
    }

}
