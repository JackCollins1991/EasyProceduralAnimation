using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    public float checkLederPositionTimer;
    public GameObject leader;

    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = checkLederPositionTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= checkLederPositionTimer)
        {
            agent.SetDestination(leader.transform.position);
            timer = 0;
        }
    }
}
