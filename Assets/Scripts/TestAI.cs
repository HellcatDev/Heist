using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BehaviourID { idle, wander, chase, investigate, patrol }

public class TestAI : MonoBehaviour
{
    public BehaviourID initialState;
    [Header("Wander Behaviour")]
    public Bounds boundBox;
    [Header("Detection")]
    public float lookRadius;
    public float soundDetectionRadius;
    public float enemyFovAngle = 45f;
    [Header("Patrol Settings")]
    public int points = 5;

    private Transform target;
    private NavMeshAgent agent;
    [SerializeField]
    private BehaviourID currentState;
    [SerializeField]
    private Vector3[] storedPatrolPoints = new Vector3[0];

    private Vector3 targetPosition = Vector3.zero;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (storedPatrolPoints.Length == 0)
        {
            storedPatrolPoints = new Vector3[points];
            for (int i = 0; i < storedPatrolPoints.Length; i++) {
                storedPatrolPoints[i] = GetRandomPosInBounds();
            }
        }
        SetState(initialState);

    }

    private void Update()
    {
        CheckLineOfSight();
        float targetDistance = Vector3.Distance(transform.position, targetPosition);
        switch (currentState)
        {
            case BehaviourID.idle:
                // idle code
                break;

            case BehaviourID.wander:
                if (targetDistance <= agent.stoppingDistance)
                {
                    GetNewWanderPoint();
                }
                break;

            case BehaviourID.chase:
                if (target != null)
                {
                    agent.SetDestination(target.position);
                }
                else
                {
                    SetState(BehaviourID.wander);
                }
                break;

            case BehaviourID.investigate:
                // investigate code
                break;

            case BehaviourID.patrol:
                if(targetDistance <= agent.stoppingDistance)
                {
                    targetPosition = GetNextPatrolPoint();
                    Debug.Log(targetPosition);
                    agent.SetDestination(targetPosition);
                }
                break;
        }
        //if (currentState == BehaviourID.wander)
        //{
        //    //controls wandering
        //    Vector3 targetDistance = transform.position - targetPosition;
        //    if (targetDistance.magnitude <= agent.stoppingDistance)
        //    {
        //        GetNewWanderPoint();
        //    }
        //}
        //else if (currentState == BehaviourID.chase)
        //{
        //    if (target != null)
        //    {
        //        agent.SetDestination(target.position);
        //    }
        //    else
        //    {
        //        SetState(BehaviourID.wander);
        //    }
        //}
        //else if (currentState == BehaviourID.investigate)
        //{
        //    //things for investigate behaviour
        //}
        //else if (currentState == BehaviourID.patrol)
        //{

        //}
    }

    private Vector3 GetNextPatrolPoint()
    {
        if (targetPosition == Vector3.zero)
        {
            return storedPatrolPoints[0];
        }
        else
        {
            Vector3 point = targetPosition;
            for (int i = 0; i < storedPatrolPoints.Length; i++)
            {
                if (storedPatrolPoints[i] == point)
                {
                    if (i + 1 >= storedPatrolPoints.Length)
                    {
                        point = storedPatrolPoints[0];
                        break;
                    }
                    else
                    {
                        point = storedPatrolPoints[i + 1];
                        break;
                    }
                }
            }
            return point;
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
                    Vector3 playerPosition = Camera.main.transform.position;
                    Vector3 vectorToPlayer = playerPosition - transform.position;
                    
                    if (Vector3.Angle(-transform.right, vectorToPlayer) <= enemyFovAngle)
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
            else if (stateID == BehaviourID.patrol)
            {
                agent.isStopped = false;
                targetPosition = storedPatrolPoints[0];
                agent.SetDestination(targetPosition);
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

        // patrol point drawing
        if (storedPatrolPoints.Length > 0)
        {
            foreach(Vector3 point in storedPatrolPoints)
            {
                if (targetPosition != point)
                {
                    Gizmos.color = new Color(1, 1, 0, 0.5f);
                    Gizmos.DrawWireSphere(point, 0.2f);
                }
                else
                {
                    Gizmos.color = new Color(0, 1, 0, 0.5f);
                    Gizmos.DrawWireSphere(point, 0.2f);
                }
            }
        }
        
    }

    private Vector3 GetRandomPosInBounds()
    {
        return new Vector3(Random.Range(-boundBox.extents.x + boundBox.center.x, boundBox.extents.x + boundBox.center.x), transform.position.y,
            Random.Range(-boundBox.extents.z + boundBox.center.z, boundBox.extents.z + boundBox.center.z));
    }
}
