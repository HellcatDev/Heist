using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float playerGravity = 15;
    public float movementSpeed = 2.0f;
    public float sprintSpeed = 3.5f;
    public float rotateSpeed = 180f;
    [Range(0f, 1f)]
    public float crouchSpeedMultiplier = 0.58f;
    public float jumpyForce = 10;
    public Stamina stam;
    public bool crouching = false;
    public static float alertLevel;

    private float currentSpeed = 0;
    private float velocity = 0f;
    private CharacterController controller;
    private Animator anim;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = movementSpeed;
    }

    /// <summary>
    /// Calculates horizontal and vertical (x and z) movement using the input from those axis. It then applies this movement
    /// using the ApplyMovement() function.
    /// </summary>
    void Update()
    {
        float _horizontalInput = Input.GetAxisRaw("Horizontal") * currentSpeed;
        float _verticalInput = Input.GetAxisRaw("Vertical") * currentSpeed;

        if (Input.GetKeyDown(KeyCode.C))
        {
            _verticalInput *= crouchSpeedMultiplier;
            _horizontalInput *= crouchSpeedMultiplier;
            if (crouching)
            {
                anim.SetBool("Crouching", false);
                crouching = false;
            }
            else
            {
                anim.SetBool("Crouching", true);
                crouching = true;
            }
        }

        if (crouching)
        {
            _verticalInput *= crouchSpeedMultiplier;
            _horizontalInput *= crouchSpeedMultiplier;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && crouching == false)
        {
            if (stam.GetCurrentStamina() > 0)
            {
                _verticalInput += sprintSpeed;
            }
            stam.Run();
        }

        if (controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) == true && crouching == false)
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

    /// <summary>
    /// This function applies movement to the transform of the player.
    /// </summary>
    /// <param name="v"></param>
    /// <param name="h"></param>
    /// <param name="velocity"></param>
    void ApplyMovement(float v, float h, float velocity)
    {
        Vector3 move = Vector3.zero;
        move += transform.forward * v * Time.deltaTime;
        move += transform.right * h * Time.deltaTime;
        move.y += velocity * Time.deltaTime;
        controller.Move(move);
    }
}
