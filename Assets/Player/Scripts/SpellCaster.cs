using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterControllerRB))]
[RequireComponent(typeof(CharacterStats))]
public class SpellCaster : MonoBehaviour
{
    private CharacterControllerRB controller;
    private CharacterStats stats;
    private Animator animator;
    private EffectEvent effectEvent;
    
    [SerializeField]
    private SpellEffect[] spells;
    
	void Start () {
   //     characterAnim = gameObject.GetComponent<CharacterAnimController>();
        controller = GetComponent<CharacterControllerRB>();
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        effectEvent = gameObject.GetComponent<EffectEvent>();
    //    spells = GetComponentsInChildren<Spell>();
    }

    public void CastSpell(string gestureName)
    {
        // znajdz gest w czarach
        // rzuć czar
        if (controller.isGrounded && !controller.isBusy && controller.hasJumpingSpace)
        {
            switch(gestureName)
            {
                case "default":
                    Spell(0);
                    break;
                case "spiral":
                    Spell(1);
                    break;
                case "six point star":
                    Spell(2);                    
                    break;
            }
        }
    }

    private void Spell(int spellNumber)
    {
        animator.SetTrigger("CastSpellTrigger");
        animator.SetInteger("Spell", spellNumber);
        var spell = Instantiate(spells[spellNumber], transform.position, transform.rotation);
        effectEvent.SetSpell(spell.GetComponent<SpellEffect>());

        StartCoroutine(IWaitForSpell());
        // TU ATTACK CZARU NASTAWIC
        var attack = spell.transform.GetComponentInChildren<Attack>();
        attack.damageDealer = stats.gameObject;
        attack.power = 50;
        //  characterAnim.CastSpell(spellNumber);
    }

    private IEnumerator IWaitForSpell()
    {
        controller.isBusy = true;
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsTag("Spell"));
        controller.BeBusy(animator.GetCurrentAnimatorStateInfo(0).length
            / animator.GetCurrentAnimatorStateInfo(0).speed);
    }

}

