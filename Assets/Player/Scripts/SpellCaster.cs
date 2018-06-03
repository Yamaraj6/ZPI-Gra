using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour {

  //  private CharacterAnimController characterAnim;
    private CharacterControllerRB controller;
    private Animator animator;
    private EffectEvent effectEvent;
    
    [SerializeField]
    private SpellEffect[] spells;
    
	void Start () {
   //     characterAnim = gameObject.GetComponent<CharacterAnimController>();
        controller = GetComponent<CharacterControllerRB>();
        animator = GetComponent<Animator>();
        effectEvent = gameObject.GetComponent<EffectEvent>();
    //    spells = GetComponentsInChildren<Spell>();
    }

    public void CastSpell(string gestureName)
    {
        // znajdz gest w czarach
        // rzuć czar
        if (controller.isGrounded) //&& !characterAnim.isCastingSpell)
        {
            switch(gestureName)
            {
                case "default":
                    Spell(0);
                    break;
                case "D":
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
        animator.SetTrigger("CastSpell");
        var spell = Instantiate(spells[spellNumber].gameObject, gameObject.transform.position, gameObject.transform.rotation);
        effectEvent.SetSpell(spell.GetComponent<SpellEffect>());
      //  characterAnim.CastSpell(spellNumber);
    }
}
