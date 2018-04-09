using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour {

    private CharacterAnimController characterAnim;
    private EffectEvent effectEvent;

    float spellDuration;

    [SerializeField]
    private Spell[] spells;
    
	void Start () {
        characterAnim = gameObject.GetComponent<CharacterAnimController>();
        effectEvent = gameObject.GetComponent<EffectEvent>();
        spells = GetComponentsInChildren<Spell>();
    }

    public void CastSpell(string gestureName)
    {
        // znajdz gest w czarach
        // rzuć czar
        if (!characterAnim.isCastingSpell)
        {
            var spell = Instantiate(spells[1].gameObject, gameObject.transform);
            effectEvent.SetSpell(spell.GetComponent<Spell>());

            characterAnim.CastSpell(1);
        }
    }
}
