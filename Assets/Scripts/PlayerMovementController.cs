using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float playerGravity = 15;
    public float movementSpeed = 2.0f;
    public float sprintSpeed = 3.5f;
    public float rotateSpeed = 180f;
    public float jumpyForce = 10;

    private float currentSpeed = 0;
    private float velocity = 0f;
    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float _horizontalInput = Input.GetAxisRaw("Horizontal") * currentSpeed;
        float _verticalInput = Input.GetAxisRaw("Vertical") * currentSpeed;

        if (controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                velocity = jumpyForce;
            }
            else
            {
                velocity = -playerGravity * Time.deltaTime;
            }
        }
        else
        {
            velocity -= playerGravity * Time.deltaTime;
        }

        ApplyMovement(_verticalInput, _horizontalInput, velocity);
    }
    void ApplyMovement(float v, float h, float velocity)
    {
        Vector3 move = Vector3.zero;
        move += transform.forward * v * Time.deltaTime;
        move += transform.right * h * Time.deltaTime;
        move.y += velocity * Time.deltaTime;
        controller.Move(move);
    }
}
