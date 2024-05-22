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
        onBoat = true;
        boatPlayerObj.SetActive(true);
        boatMovement.enabled = true;
        flc.Follow = boatPlayerObj.transform;
        flc.LookAt = boatPlayerObj.transform;
        realPlayer.SetActive(false);
    }

    public void ExitBoat()
    {
        realPlayer.transform.position = transform.position;
        onBoat = false;
        realPlayer.SetActive(true);
        flc.Follow = realPlayer.transform;
        flc.LookAt = realPlayer.transform;
        boatPlayerObj.SetActive(false);
        boatMovement.enabled = false;
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
