using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterAnimController : MonoBehaviour
{
    public bool isCastingSpell { get; private set; } = false;
    private bool flagSpell = false;

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

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("CastSpell") && flagSpell)
        {
            StartCoroutine(WaitForAnimation(
                    anim.GetCurrentAnimatorStateInfo(0).length,
                    anim.GetCurrentAnimatorStateInfo(0).speed));
            flagSpell = false;
        }
    }


    private void Running()
    {
        if (CrossPlatformInputManager.GetButton("Right"))
        {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsRunningBack", false);
        }
        else if (CrossPlatformInputManager.GetButton("Left"))
        {
            anim.SetBool("IsRunningBack", true);
            anim.SetBool("IsRunning", false);
        }
        else 
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
                    anim.GetCurrentAnimatorStateInfo(0).speed));
        }
    }

    public void CastSpell(int spellNuber)
    {
        if (isCastingSpell || flagSpell)
        {
            return;
        }
        flagSpell = true;
        isCastingSpell = true;
        anim.SetInteger("SpellNumber", spellNuber);
        anim.SetTrigger("CastSpell");
    }        

    private IEnumerator WaitForAnimation(float length, float speed)
    {
        var tempTime = length * (1 / speed);
        yield return new WaitForSeconds(tempTime);
        isCastingSpell = false;
    }
}