using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegManager : MonoBehaviour
{
    public LayerMask solidLayer;
    public Vector3 legDirection;
    public bool smoothMode;
    public float maxLegDistance;
    public float stepCooldown;
    public float cooldownShift;
    public float legLength;
    public float groundFinder;
    public Material mMaterial;
    public float mWidth;
    public float stepTime;

    private float timer;
    private Vector3 footPosition;
    private LineRenderer lr;
    private Vector3 desiredPosition;
    private Vector3 startPosition;
    private float animationTimer;

    void Start()
    {
        //initialize the starting value of variables
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        timer = stepCooldown - cooldownShift;
        animationTimer = stepTime;
        lr.material = mMaterial;
        lr.startWidth = mWidth;
        lr.endWidth = mWidth;
        startPosition = transform.TransformPoint(legDirection); 
    }

    void Update()
    {
        //convert global Vector3 value to local
        Vector3 direction = transform.TransformPoint(legDirection);
        //count down timers
        timer -= Time.deltaTime;
        animationTimer -= Time.deltaTime;

        if (timer <= 0.0f) {
            // set the start position variable to be whatever is currently in the desiredposition variable, then re-assign.
            startPosition = desiredPosition;
            desiredPosition = GetGroundPosition(direction);
            timer = stepCooldown;
            animationTimer = stepTime;
        }
        if(Vector3.Distance(transform.position, footPosition) > maxLegDistance){
            //ensures leg is never onger than max length
            desiredPosition = GetGroundPosition(direction);
        }
        if (smoothMode)
        {
            //uses linear interpolation to set the leg to points from the starting position, to the desired destination. leg is at desired position when the step time is completed. 
            footPosition = Vector3.Lerp(startPosition, desiredPosition, ((stepTime - timer) / stepTime));
        }
        else { footPosition = desiredPosition; }

        //set positions
        Vector3[] p = { transform.position, footPosition };
        lr.SetPositions(p);

        Debug.DrawLine(legDirection, legDirection + (Vector3.down*legLength), Color.red, 1.0f);

    }


    public Vector3 GetGroundPosition(Vector3 position)
    {

        RaycastHit hit;
        if (Physics.SphereCast(position, groundFinder, Vector3.down, out hit, legLength))
        {
            position = hit.point;
        }
        else
        {
            //position = transform.TransformPoint(legDirection);
        }
        
        return position;
    }
}
