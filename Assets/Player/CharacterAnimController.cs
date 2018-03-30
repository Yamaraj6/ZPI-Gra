using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    private Animator anim;
    private PlayerDetector playerDetector;

    private float move = 0;
    private bool isJumping = false;
    private bool isReturning = false;

    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    private float jumpingSpeed = 1.2f;

    [SerializeField]
    private float rotationSpeed = 15;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        playerDetector = GetComponent<PlayerDetector>();
    }


    // Update is called once per frame
    void Update()
    {
            Running();
            Jump();
    }


    private void Running()
    {
        float translation;
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {
            move = Input.GetAxis("Horizontal");
            translation = move * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log(move);
            translation = move *
            speed * Time.deltaTime * jumpingSpeed;
        }

        if (true&& translation > 0)
        {
            transform.Translate(0, 0, translation);
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsRunningBack", false);
        }
        else if (translation < 0)
        {
            transform.Translate(0, 0, translation);
            anim.SetBool("IsRunningBack", true);
            anim.SetBool("IsRunning", false);
        }
        else if(!Input.GetKeyDown(KeyCode.LeftArrow)
            && !Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsRunningBack", false);
        }
    }   

    private void Jump()
    {
        if (!isJumping 
            && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump") 
            && Input.GetAxis("Vertical") > 0)
        {
            isJumping = true;
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
        isJumping = false;
    }
}