using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        Running();
        Jump();
    }


    private void Running()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsRunningBack", false);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("IsRunningBack", true);
            anim.SetBool("IsRunning", false);
        }
        else if (!Input.GetKeyDown(KeyCode.LeftArrow)
            && !Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsRunningBack", false);
        }
    }

    private void Jump()
    {
        if (playerController.isJumping && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {
            anim.SetTrigger("Jump");
            StartCoroutine(WaitForAnimation(
                    anim.GetCurrentAnimatorStateInfo(0).length,
                    anim.GetCurrentAnimatorStateInfo(0).speed, "IsJumping"));
        }
    }

    private IEnumerator WaitForAnimation(float length, float speed, string paramName)
    {
        var tempTime = length * (1 / speed);
        yield return new WaitForSeconds(tempTime * 0.75f);
    }
}