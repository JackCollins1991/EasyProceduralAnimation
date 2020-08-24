using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraAround : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        Vector3 direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        transform.position = target.transform.position + Vector3.forward * range;
        transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}

