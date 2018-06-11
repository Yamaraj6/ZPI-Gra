using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterControllerRB))]
public class CharacterControllerJump : MonoBehaviour
{
    //Components.
    private Rigidbody rb;
    private Animator animator;
    private CharacterControllerRB characterController;

    public float gravity = -9.8f;
    public float jumpSpeed = 12;
    float fallingVelocity = -1f;
    bool canJump = true;
    bool isJumping = false;
    bool jumped = false;
    bool isFalling;
    bool startFall;
    public float maxFallingVelocity = 10;
    public float maxInAirVelocity = 5;
    public float inAirSpeed = 2;
    public float jumpDelay = 0.1f;


    //Input variables.
    float inputHorizontal = 0f;
    float inputVertical = 0f;
    bool inputJump;


    // Use this for initialization
    void Start()
    {
        characterController = GetComponentInChildren<CharacterControllerRB>();
        rb = GetComponentInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterController.isBusy)
        {
            Inputs();
            Fall();
            Jumping();
            Landing();
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y > -maxFallingVelocity)
        {
            rb.AddForce(0, gravity, 0, ForceMode.Acceleration);
        }
        AirControl();
    }

    void Inputs()
    {
        inputHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        inputVertical = CrossPlatformInputManager.GetAxis("Vertical");
        inputJump = CrossPlatformInputManager.GetButtonDown("Jump");
    }

    void Landing()
    {
        if ((jumped && !isJumping && characterController.isGrounded)
            || isFalling && characterController.isGrounded)
        {
            animator.SetInteger("Jumping", 0);
            ResetVaribles();
            StartCoroutine(_Land());
        }
    }

    void ResetVaribles()
    {
        startFall = false;
        isFalling = false;
        jumped = false;
    //    characterController.isBusy = false;
    }

    //Check if falling.
    private void Fall()
    {
        if ((rb.velocity.y < fallingVelocity
            || (jumped && !isJumping))
            && !startFall && !characterController.isGrounded)
        {
            startFall = true;
         //   characterController.isBusy = true;
            isFalling = true;
            animator.SetInteger("Jumping", 2);
            animator.SetTrigger("JumpTrigger");
            canJump = false;
        }
    }

    private void Jumping()
    {
        if (characterController.isGrounded && characterController.hasJumpingSpace
            && canJump /*&& !characterController.isBusy */&& inputJump)
        {
            StartCoroutine(_Jump());
        }
    }

    public IEnumerator _Jump()
    {
        jumped = true;
        isJumping = true;
      //  characterController.isBusy = true;
        animator.SetInteger("Jumping", 1);
        animator.SetTrigger("JumpTrigger");
        //Apply the current movement to launch velocity.
        canJump = false;
        yield return new WaitForSeconds(jumpDelay);
        rb.velocity += jumpSpeed * Vector3.up;
        yield return new WaitForSeconds(.5f);
        isJumping = false;
    }

    public IEnumerator _Land()
    {
        yield return new WaitForSeconds(.2f);
        canJump = true;
    }

    public void AirControl()
    {
        if (!characterController.isGrounded)
        {
            float axisX = CrossPlatformInputManager.GetAxis("Horizontal");
            float axisZ = CrossPlatformInputManager.GetAxis("Vertical");

            Vector3 motion = Vector3.zero;
            motion = new Vector3(axisX, 0, axisZ);
            motion = transform.TransformDirection(motion);
            if (motion.x > 0)
            {
                if (rb.velocity.x >= maxInAirVelocity)
                {
                    motion.x = 0;
                }
            }
            else
            {
                if (rb.velocity.x <= -maxInAirVelocity)
                {
                    motion.x = 0;
                }
            }
            if (motion.z > 0)
            {
                if (rb.velocity.z >= maxInAirVelocity)
                {
                    motion.z = 0;
                }
            }
            else
            {
                if (rb.velocity.z <= -maxInAirVelocity)
                {
                    motion.z = 0;
                }
            }

            rb.AddForce(motion * inAirSpeed, ForceMode.Acceleration);
        }
    }
}