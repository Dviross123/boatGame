using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask interactionLayer;


    void Update()
    {
        CheckForInteractions();
    }

    void CheckForInteractions()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, interactionLayer);

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Boat"))
                {
                    hitCollider.GetComponent<BoatManager>().EnterBoat();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
