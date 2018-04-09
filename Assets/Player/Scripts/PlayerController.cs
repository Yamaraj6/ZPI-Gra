using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public bool isJumping { get; private set; } = false;

        private CharacterController controller;
    private CharacterAnimController characterAnimController;

    private float gravity = 14.0f;
    private float jumpForce = 5.0f;    


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
    void LateUpdate()
    {
        Running();
        Jump();
        controller.Move(new Vector3(moveVelocity, verticalVelocity, 0) * Time.deltaTime);
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
}