using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BehaviourID { idle, wander }

public class TestAI : MonoBehaviour
{
    public BehaviourID initialState;
    [Header("Wander Behaviour")]
    public Bounds boundBox;

    private Transform target;
    private NavMeshAgent agent;
    private BehaviourID currentState;

    private Vector3 targetPosition = Vector3.zero;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetState(initialState);
    }

    private void Update()
    {
        if (currentState == BehaviourID.wander)
        {
            Vector3 targetDistance = transform.position - targetPosition;
            if (targetDistance.magnitude <= agent.stoppingDistance)
            {
                GetNewWanderPoint();
            }
        }
    }

    public void SetState(BehaviourID stateID)
    {
        if(currentState != stateID)
        {
            //set our current state id to the one being passed
            //initialize behaviour according to state id
            if(stateID == BehaviourID.idle)
            {
                //idle
            }
            else if (stateID == BehaviourID.wander)
            {
                //wander
                GetNewWanderPoint();
            }
            currentState = stateID;
        }
    }

    void GetNewWanderPoint()
    {
        targetPosition = GetRandomPosInBounds();
        agent.SetDestination(targetPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(targetPosition, 0.2f);
    }

    private Vector3 GetRandomPosInBounds()
    {
        return new Vector3(Random.Range(-boundBox.extents.x + boundBox.center.x, boundBox.extents.x + boundBox.center.x), transform.position.y,
            Random.Range(-boundBox.extents.z + boundBox.center.z, boundBox.extents.z + boundBox.center.z));
    }
}
