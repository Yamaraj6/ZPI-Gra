using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour {

    private CharacterAnimController characterAnim;
    private EffectEvent effectEvent;
    
    [SerializeField]
    private Spell[] spells;
    
	void Start () {
        characterAnim = gameObject.GetComponent<CharacterAnimController>();
        effectEvent = gameObject.GetComponent<EffectEvent>();
    //    spells = GetComponentsInChildren<Spell>();
    }

    public void CastSpell(string gestureName)
    {
        // znajdz gest w czarach
        // rzuć czar
        if (!characterAnim.isCastingSpell)
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
        var spell = Instantiate(spells[spellNumber].gameObject);
        effectEvent.SetSpell(spell.GetComponent<Spell>());
        characterAnim.CastSpell(spellNumber);
    }
}
