using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public static bool bossArea = false; //Kori
    public bool isJumping { get; private set; } = false;

    private CharacterController controller;
    private CharacterAnimController characterAnimController;

    private float gravity = 14.0f;
    private float jumpForce = 6.0f;

    [SerializeField]
    private float speed = 4.0f;

    [SerializeField]
    private float jumpingSpeed = 1.2f;


    private float moveVelocity = 0;
    private float verticalVelocity = 0;


    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        characterAnimController = GetComponent<CharacterAnimController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossArea)
        {
            Running();
            Jump();
            controller.Move(new Vector3(moveVelocity, verticalVelocity, 0) * Time.deltaTime);
        }
        else
        {
            //Kori
            LookAtBoss();
            MoveInBossArea();
            //controller.Move(new Vector3(moveVelocity, 0, verticalVelocity));
        }


    }

    private void Running()
    {
        if (characterAnimController.isCastingSpell)
        {
            moveVelocity = 0;
            return;
        }
        float move = CrossPlatformInputManager.GetAxis("Horizontal");
        if (controller.isGrounded)
        {
            moveVelocity = move * speed;
        }
        else
        {
            moveVelocity = move *
            speed * jumpingSpeed;
        }
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (CrossPlatformInputManager.GetButtonDown("Jump") && !characterAnimController.isCastingSpell)
            {
                verticalVelocity = jumpForce;
                isJumping = true;
            }
        }
        else
        {
            isJumping = false;
            verticalVelocity -= gravity * Time.deltaTime;
        }
    }

    //Kori
    private void MoveInBossArea()
    {
        float axisX = CrossPlatformInputManager.GetAxis("Horizontal");
        float axisZ = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 motion = Vector3.zero;
        motion = new Vector3(axisX, 0, axisZ);
        motion = transform.TransformDirection(motion) * speed;

        if (controller.isGrounded)
        {
            verticalVelocity = 0;
            if (CrossPlatformInputManager.GetButtonDown("Jump") && !characterAnimController.isCastingSpell)
            {
                verticalVelocity = jumpForce;
                isJumping = true;
            }
        }

        verticalVelocity -= gravity * Time.deltaTime;

        motion.y = verticalVelocity;
        controller.Move(motion * Time.deltaTime);
    }

    //Kori
    private void LookAtBoss()
    {
        if (bossArea)
        {
            Vector3 _target = GameObject.FindGameObjectWithTag("Boss").transform.position;
            _target.y = transform.position.y;
            transform.LookAt(_target);
        }
    }
}