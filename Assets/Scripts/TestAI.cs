using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BehaviourID { idle, wander, chase, investigate }

public class TestAI : MonoBehaviour
{
    public BehaviourID initialState;
    [Header("Wander Behaviour")]
    public Bounds boundBox;
    [Header("Detection")]
    public float lookRadius;
    public float soundDetectionRadius;
    public float enemyFovAngle = 45f;

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
        CheckLineOfSight();
        if (currentState == BehaviourID.wander)
        {
            //controls wandering
            Vector3 targetDistance = transform.position - targetPosition;
            if (targetDistance.magnitude <= agent.stoppingDistance)
            {
                GetNewWanderPoint();
            }
        }
        else if (currentState == BehaviourID.chase)
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
            else
            {
                SetState(BehaviourID.wander);
            }
        }
        else if (currentState == BehaviourID.investigate)
        {
            //things for investigate behaviour
        }
    }

    private void CheckLineOfSight()
    {
        if (target == null)
        {
            //controls player detection
            Collider[] colliderCheck = Physics.OverlapSphere(transform.position, lookRadius);
            foreach (Collider col in colliderCheck)
            {
                if (col.CompareTag("Player") == true)
                {
                    Vector3 fovRadius = gameObject.transform.forward * lookRadius;
                    float distanceToPlayer = Vector3.Distance(Camera.main.transform.position, fovRadius);
                    float playerAngle = Vector3.Angle(Camera.main.transform.position, fovRadius);
                    //Vector3 dir = col.transform.position - transform.position; how to calculate direction :)
                    if (playerAngle < enemyFovAngle)
                    {
                        if (Physics.Linecast(transform.position, col.transform.position, out RaycastHit hit))
                        {
                            if (hit.transform.CompareTag("Player") == true)
                            {
                                target = col.transform;
                                SetState(BehaviourID.chase);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance > lookRadius)
            {
                if (Physics.Linecast(transform.position, target.transform.position, out RaycastHit hit))
                {
                    if (hit.transform.CompareTag("Player") == true)
                    {
                        target = hit.transform;
                        SetState(BehaviourID.chase);
                    }
                    else
                    {
                        SetState(BehaviourID.wander);
                        agent.SetDestination(target.position);
                        targetPosition = target.position;
                        target = null;
                    }
                }
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
                agent.isStopped = true;
            }
            else if (stateID == BehaviourID.wander)
            {
                //wander
                agent.isStopped = false;
                GetNewWanderPoint();
            }
            else if (stateID == BehaviourID.chase)
            {
                agent.isStopped = false;
            }
            else if (stateID == BehaviourID.investigate)
            {
                agent.isStopped = false;
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
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, soundDetectionRadius);
    }

    private Vector3 GetRandomPosInBounds()
    {
        return new Vector3(Random.Range(-boundBox.extents.x + boundBox.center.x, boundBox.extents.x + boundBox.center.x), transform.position.y,
            Random.Range(-boundBox.extents.z + boundBox.center.z, boundBox.extents.z + boundBox.center.z));
    }
}
