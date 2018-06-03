using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerDamage : MonoBehaviour
{
    //Components.
    private CharacterStats playerStats;
    private CapsuleCollider collider;
    private Animator animator;
    private CharacterControllerRB characterController;

    public float getHitDuration = 0.2f;


    void Start()
    {
        playerStats = GetComponent<CharacterStats>();
        characterController = GetComponent<CharacterControllerRB>();
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var _attack = other.transform.GetComponent<Attack>();
        if (other.tag == "Attack" && 
            (_attack.damageDealer == null  || _attack.damageDealer.tag != gameObject.tag))
        {
            GetHit(_attack);
        }
        else if(other.tag == "PowerUp")
        {

        }
    }

    private void GetHit(Attack attack)
    {
        playerStats.DealDamage(attack);
        if (playerStats.IsDead())
        {
            StartCoroutine(_Die());
        }
        else
        {
            StartCoroutine(_GetHit());
        }
    }

    public IEnumerator _GetHit()
    {
        characterController.isBusy = true;
        yield return new WaitUntil(() =>characterController.isGrounded);
        animator.SetTrigger("GetHitTrigger");
        yield return new WaitForSeconds(getHitDuration);
        characterController.isBusy = false;
    }

    public IEnumerator _Die()
    {
        characterController.isBusy = true;
        yield return new WaitUntil(() => characterController.isGrounded);
        animator.SetTrigger("DeathTrigger");
    }
}
