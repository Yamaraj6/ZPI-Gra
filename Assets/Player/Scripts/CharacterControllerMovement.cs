using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterControllerMovement : MonoBehaviour
{
	//Components.
	private Rigidbody rb;
	private CapsuleCollider collider;
	private Animator animator;
	private CharacterControllerRB characterController;

	//Moving 
	public float idleStartTime = 0.2f;
	private float stopTime = 0;
	public float runSpeed = 200f;

	//Sneaking
	public float sneakSpeed = 100f;
	public float heightInSneaking = 1f;
	private float normalHeight;
	public float colliderCenterInSneaking = 0.52f;
	private float colliderCenter;


	//Animator const.
	public float damping = 0.15f;

	//Input variables.
	float inputHorizontal = 0f;
	float inputVertical = 0f;
	bool inputSneaking = false;
	private Vector3 inputVec;

	// Use this for initialization
	void Start ()
	{
		characterController = GetComponentInChildren<CharacterControllerRB>();
		rb = GetComponentInChildren<Rigidbody>();
		collider = GetComponent<CapsuleCollider>();
		normalHeight = collider.height;
		colliderCenter = collider.center.y;
		animator = GetComponentInChildren<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (!characterController.isBusy)
        {
            Inputs();
            Move();
        }
        else
        {
            MovePhysic(new Vector3(0,0,0),0);
        }
    }

	void Inputs()
	{
		inputHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		inputVertical = CrossPlatformInputManager.GetAxis("Vertical");
		if (CrossPlatformInputManager.GetButtonDown("Sneak"))
		{
			if (inputSneaking)
			{
				if (characterController.hasJumpingSpace)
				{ inputSneaking = false; }
			}
			else { inputSneaking = true;}
		}
		inputVec = new Vector3(inputHorizontal, 0, inputVertical).normalized;
	}

	private void Move()
	{
		var isSneaking = Sneak();
		if (characterController.isGrounded && !isSneaking)
		{
			MovePhysic(inputVec, runSpeed);
		}
		
		if (characterController.isGrounded && (inputHorizontal != 0 || inputVertical != 0))
		{
			//Move Animation
			animator.SetBool("Moving", true);
			animator.SetFloat("Horizontal", inputVec.x, damping, Time.deltaTime);
			animator.SetFloat("Vertical", inputVec.z, damping, Time.deltaTime);
			stopTime = 0;
		}
		else
		{
			if (stopTime > idleStartTime)
			{
				animator.SetBool("Moving", false);
			}
			stopTime += Time.deltaTime;
		}
	}

	private void MovePhysic(Vector3 inputVec, float speed)
	{
		Vector3 motion = Vector3.zero;
		motion = new Vector3(inputVec.x, 0, inputVec.z);
		motion = transform.TransformDirection(motion) * speed;
		rb.velocity = new Vector3(motion.x * Time.deltaTime,rb.velocity.y, motion.z * Time.deltaTime);
	}

	private bool Sneak()
	{
		if(inputSneaking)
		{
			animator.SetBool("Sneaking", true);
			MovePhysic(inputVec, sneakSpeed);
			collider.height = heightInSneaking;
			collider.center = new Vector3(0, colliderCenterInSneaking, 0);
			return true;
		}
		else
		{
			animator.SetBool("Sneaking", false);
			collider.height = normalHeight;
			collider.center=new Vector3(0,colliderCenter,0);
		}
		return false;
	}
}
