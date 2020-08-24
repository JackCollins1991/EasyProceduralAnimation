using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    public float maxRadiansDelta;
    public float maxMagnitudeDelta;
    public GameObject owner;
    public enum Mode
    {
        Random,
        Direction,
        AtPlayer
    }
    public Mode mode;
    public float timer;
    private float t;
    private Vector3 newDirection;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        t = timer;
        direction = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {

        if (mode == Mode.Direction) {
            LookAtDestination();
        }
        else if (mode == Mode.AtPlayer) {
            LookAtPlayer();
        }
        else if (mode == Mode.Random){
            LookAtRandom();
        }
        Debug.DrawLine(transform.position, direction, Color.white, 0.0f);
    }
    public void LookAtDestination() {
        direction = owner.transform.forward;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    public void LookAtPlayer() { 
    
    }

    public void LookAtRandom() {

        t -= Time.deltaTime;
        float singleStep = maxRadiansDelta * Time.deltaTime;
        if (t < 0.0f) {
            var q = Random.rotation;  
            direction = q.eulerAngles;
            t = timer;
        }
        newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, maxMagnitudeDelta);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
