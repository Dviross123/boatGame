using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floater : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBefSub;
    public float displacementAmt;
    public int floaters;

    public float waterDrag;
    public float waterAngularDrag;
    public WaterSurface water;
    WaterSearchParameters Search;
    WaterSearchResult SearchResult;

    [SerializeField] Transform[] points; 

    private void FixedUpdate()
    {

        rb.AddForceAtPosition(Physics.gravity / floaters, transform.position, ForceMode.Acceleration);

        foreach (Transform point in points)
        {
            Search.startPositionWS = point.position;

            water.ProjectPointOnWaterSurface(Search, out SearchResult);

            if (point.position.y < SearchResult.projectedPositionWS.y)
            {
                float displacementMulti = Mathf.Clamp01(SearchResult.projectedPositionWS.y - point.position.y / depthBefSub) * displacementAmt;
                
                rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMulti, 0f), point.position, ForceMode.Acceleration);
              
                rb.AddForce(displacementMulti * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
                rb.AddTorque(displacementMulti * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
        }
    }
}
