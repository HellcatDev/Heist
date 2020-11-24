using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum BehaviourID { idle, wander, chase, investigate, patrol, alerted }

public class AI : MonoBehaviour
{
    [Header("AI Settings")]
    public float walkSpeed = 1.2f;
    public float runSpeed = 1.7f;
    public BehaviourID initialState = BehaviourID.idle;
    [Header("Bound Boxes")]
    public Bounds boundBox;
    private Bounds investigateBox;
    public float investigateBoxSize;
    [Header("Detection Settings")]
    public float viewRadius;
    public float initialViewAngle = 60f;
    private float viewAngle;
    public float soundDetectionRadius;
    public LayerMask sightDontIgnore;
    [Header("Patrol Settings")]
    public int points = 5;

    [SerializeField]
    private Vector3[] storedInvestigatePoints = new Vector3[0];
    [Header("Target Settings")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 targetPosition = Vector3.zero;
    private NavMeshAgent agent;
    private Animator animator;
    [Header("State")]
    [SerializeField]
    private BehaviourID currentState;
    private float timeIdle = 0f;
    private Vector3 debugMiddle;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        investigateBox.size = new Vector3(investigateBoxSize, 1f, investigateBoxSize);

        SetState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        debugMiddle = new Vector3(transform.position.x, agent.height / 2, transform.position.z);
        CheckLineOfSight();
        if (target != null)
        {
            targetPosition = target.position;
        }
        float targetDistance = Vector3.Distance(transform.position, targetPosition);
        switch (currentState)
        {
            case BehaviourID.idle:
                viewAngle = initialViewAngle;
                if (Time.time >= timeIdle)
                {
                    SetState(BehaviourID.wander);
                }
                break;
            case BehaviourID.wander:
                viewAngle = initialViewAngle;
                if (targetDistance <= agent.stoppingDistance + 0.2f)
                {
                    SetState(BehaviourID.idle);
                }
                break;
            case BehaviourID.chase:
                viewAngle = initialViewAngle;
                if (target != null)
                {
                    agent.SetDestination(targetPosition);
                    if (targetDistance <= agent.stoppingDistance + 0.2f)
                    {
                        SetState(BehaviourID.idle);
                    }
                }
                else
                {
                    SetState(BehaviourID.wander);
                }
                break;
            case BehaviourID.investigate:
                // AI is already running to first target.
                // if AI is close to target
                // start investigating
                // while investigating do nothing
                // if investigating has stopped
                // get new point and start running
                // repeat
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    if (targetDistance <= agent.stoppingDistance)
                    {
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Look Around") == false) // if animation is not already playing
                        {
                            animator.SetTrigger("Look Around"); // play animation
                            viewAngle = 150f;
                            agent.isStopped = true; // stop AI
                            break;
                        }
                        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && animator.GetCurrentAnimatorStateInfo(0).IsName("Look Around") == true) // if animation has finished playing
                        {
                            targetPosition = GetNextPatrolPoint(); // get next point
                            viewAngle = initialViewAngle;
                            if (targetPosition == Vector3.zero) // if all patrol points have been cycled through
                            {
                                SetState(BehaviourID.wander); // wander
                                break;
                            }
                            animator.SetTrigger("Run"); // play run animation
                            agent.isStopped = false; // start AI
                            agent.SetDestination(targetPosition); // set AI destination
                        }
                    }

                }
                break;




                //if (targetDistance <= agent.stoppingDistance)
                //{
                //    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.2f)
                //    {
                //        animator.SetTrigger("Look Around");
                //        agent.isStopped = true;
                //        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                //        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.2f)
                //        {
                //            agent.isStopped = false;
                //            animator.SetTrigger("Run");
                //            targetPosition = GetNextPatrolPoint();
                //            if (targetPosition == Vector3.zero)
                //            {
                //                SetState(BehaviourID.wander);
                //                break;
                //            }
                //            agent.SetDestination(targetPosition);
                //        }
                //    }
                //}
                //break;
        }
    }

    private void CheckLineOfSight()
    {
        if (CanSeePlayer() == true) // Self explanatory, can see player is true.
        {
            if (target == null) // If AI has no target set
            {
                target = Camera.main.transform.root; // set AI target to the player
                SetState(BehaviourID.chase); // set AI state to chase
            }
            else if (currentState == BehaviourID.wander)
            {
                target = Camera.main.transform.root;
                SetState(BehaviourID.chase);
            }
        }
        else // if AI can't see the player
        {
            if (target != null) // and target is not null
            {
                targetPosition = target.position; // set targetPosition to the last known position of the player
                target = null; // target will now equal null
                SetState(BehaviourID.investigate); // set AI state to investigate the last known position area.
            }
        }
    }

    private bool CanSeePlayer()
    {
        Vector3 playerPosition = Camera.main.transform.root.position;
        Vector3 vectorToPlayer = playerPosition - transform.position;
        if (Vector3.Distance(debugMiddle, playerPosition) <= soundDetectionRadius)
        {
            if (Physics.Linecast(transform.position, playerPosition, out RaycastHit hit, sightDontIgnore))
            {
                if (hit.transform.CompareTag("Player") == true)
                {
                    if (hit.transform.GetComponent<PlayerMovementController>().crouching == false)
                    {
                        Debug.DrawLine(debugMiddle, playerPosition, Color.red);
                        return true;
                    }
                    Debug.DrawLine(debugMiddle, playerPosition, Color.yellow);
                    return false;
                }
                Debug.DrawLine(debugMiddle, playerPosition, Color.yellow);
                return false;
            }
        }
        if (Vector3.Distance(debugMiddle, playerPosition) <= viewRadius || target != null)
        {
            if (Vector3.Angle(transform.forward, vectorToPlayer) <= viewAngle || target != null)
            {
                if (Physics.Linecast(transform.position, playerPosition, out RaycastHit hit, sightDontIgnore))
                {
                    if (hit.transform.CompareTag("Player") == true)
                    {
                        Debug.DrawLine(debugMiddle, playerPosition, Color.red);
                        return true;
                    }
                    Debug.DrawLine(debugMiddle, playerPosition, Color.yellow);
                    return false;
                }
            }
            else
            {
                Debug.DrawLine(debugMiddle, playerPosition, Color.green);
                return false;
            }
        }
        return false;
    }

    private Vector3 GetNextPatrolPoint()
    {
        if (targetPosition == Vector3.zero)
        {
            return storedInvestigatePoints[0];
        }
        else
        {
            Vector3 point = targetPosition;
            for (int i = 0; i < storedInvestigatePoints.Length; i++)
            {
                if (storedInvestigatePoints[i] == point)
                {
                    if (i + 1 >= storedInvestigatePoints.Length)
                    {
                        point = Vector3.zero;
                        break;
                    }
                    else
                    {
                        point = storedInvestigatePoints[i + 1];
                        break;
                    }
                }
            }
            return point;
        }
    }

    public void SetState(BehaviourID stateID)
    {
        if (currentState != stateID)
        {
            //set our current state id to the one being passed
            //initialize behaviour according to state id
            if (stateID == BehaviourID.idle)
            {
                animator.SetTrigger("Idle");
                timeIdle = Time.time + Random.Range(1f, 7f);
                agent.isStopped = true;
            }
            else if (stateID == BehaviourID.wander)
            {
                animator.SetTrigger("Walk");
                agent.speed = walkSpeed;
                agent.isStopped = false;
                GetNewWanderPoint();
            }
            else if (stateID == BehaviourID.chase)
            {
                animator.SetTrigger("Run");
                agent.speed = runSpeed;
                agent.isStopped = false;
            }
            else if (stateID == BehaviourID.investigate)
            {
                SetupInvestigatePoints();
                animator.SetTrigger("Run");
                agent.speed = runSpeed;
                agent.isStopped = false;
                targetPosition = storedInvestigatePoints[0];
                agent.SetDestination(targetPosition);
            }
            //else if (stateID == BehaviourID.patrol)
            //{
            //    animator.SetTrigger("Walk");
            //    agent.speed = walkSpeed;
            //    agent.isStopped = false;
            //    targetPosition = storedInvestigatePoints[0];
            //    agent.SetDestination(targetPosition);
            //}
            currentState = stateID;
        }
    }

    void SetupInvestigatePoints()
    {
        investigateBox.center = new Vector3(targetPosition.x, 0.5f, targetPosition.z);
        storedInvestigatePoints = new Vector3[points];
        for (int i = 0; i < storedInvestigatePoints.Length; i++)
        {
            storedInvestigatePoints[i] = GetRandomPosInInvestigateBounds();
        }
    }

    void GetNewWanderPoint()
    {
        targetPosition = GetRandomPosInBounds();
        agent.SetDestination(targetPosition);
    }

    private Vector3 GetRandomPosInBounds()
    {
        Vector3 newPosition = new Vector3(Random.Range(-boundBox.extents.x + boundBox.center.x, boundBox.extents.x + boundBox.center.x), transform.position.y,
            Random.Range(-boundBox.extents.z + boundBox.center.z, boundBox.extents.z + boundBox.center.z));
        while (agent.CalculatePath(newPosition, agent.path) == false)
        {
            newPosition = new Vector3(Random.Range(-boundBox.extents.x + boundBox.center.x, boundBox.extents.x + boundBox.center.x), transform.position.y,
            Random.Range(-boundBox.extents.z + boundBox.center.z, boundBox.extents.z + boundBox.center.z));
        }
        return newPosition;
    }

    /// <summary>
    /// Returns a new position within the investigate bounds
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPosInInvestigateBounds()
    {
        Vector3 newPosition = new Vector3(Random.Range(-investigateBox.extents.x + investigateBox.center.x, investigateBox.extents.x + investigateBox.center.x), transform.position.y,
            Random.Range(-investigateBox.extents.z + investigateBox.center.z, investigateBox.extents.z + investigateBox.center.z));
        while (agent.CalculatePath(newPosition, agent.path) == false)
        {
            newPosition = new Vector3(Random.Range(-investigateBox.extents.x + investigateBox.center.x, investigateBox.extents.x + investigateBox.center.x), transform.position.y,
            Random.Range(-investigateBox.extents.z + investigateBox.center.z, investigateBox.extents.z + investigateBox.center.z));
        }
        return newPosition;
    }

    private void OnDrawGizmos()
    {
        Vector3 middlePosition = new Vector3(transform.position.x, 0.87f, transform.position.z);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(targetPosition, 0.2f);
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(middlePosition, soundDetectionRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(investigateBox.center, investigateBox.size);
        //if (Application.isEditor)
        //{
        //    Handles.color = Color.white;
        //    Handles.DrawWireArc(middlePosition, Vector3.up, Vector3.forward, 360, viewRadius);
        //    Handles.color = new Color(1, 0, 0, 0.2f);
        //    Handles.DrawSolidArc(middlePosition, Vector3.up, transform.forward, viewAngle, viewRadius);
        //    Handles.DrawSolidArc(middlePosition, Vector3.up, transform.forward, -viewAngle, viewRadius);
        //}
        // patrol point drawing
        if (storedInvestigatePoints.Length > 0)
        {
            foreach (Vector3 point in storedInvestigatePoints)
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
}
