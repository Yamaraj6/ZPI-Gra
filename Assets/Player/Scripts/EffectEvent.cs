using UnityEngine;
using System.Collections;

public class EffectEvent : MonoBehaviour
{    
    public Transform CharacterAttachPoint;    
    public Transform CharacterAttachPoint2;    
    public Transform AttachPoint;    
    public Transform AdditionalEffectAttachPoint;

    private Spell spell;

    public void SetSpell(Spell spell)
    {
        this.spell = spell;
        spell.SetupSpellPosition(gameObject.transform);
    }

    public void ActivateEffect()
    {
        if (spell != null)
        {
            spell.ActivateEffect();
        }
    }

    public void ActivateAdditionalEffect()
    {
        if (spell != null)
        {
            spell.ActivateAdditionalEffect();
        }
    }

    public void ActivateCharacterEffect()
    {
        if (spell != null)
        {
            spell.ActivateCharacterEffect();
        }
    }

    public void ActivateCharacterEffect2()
    {
        if (spell != null)
        {
            spell.ActivateCharacterEffect2();
        }
    }

    void LateUpdate()
    {
        if (spell != null)
        {
            spell.UpdateSpellPosition(CharacterAttachPoint, CharacterAttachPoint2,
            AttachPoint, AdditionalEffectAttachPoint);
        }
    }
}