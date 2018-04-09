using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {
    [SerializeField]
    private GameObject CharacterEffect;
    [SerializeField]
    private GameObject CharacterEffect2;
    [SerializeField]
    private GameObject Effect;
    [SerializeField]
    private GameObject AdditionalEffect;

    private void OnEnable()
    {
        if (Effect != null)
        {
            Effect.SetActive(false);
        }
        if (AdditionalEffect != null)
        {
            AdditionalEffect.SetActive(false);
        }
        if (CharacterEffect != null)
        {
            CharacterEffect.SetActive(false);
        }
        if (CharacterEffect2 != null)
        {
            CharacterEffect2.SetActive(false);
        }
    }

    public void ActivateEffect()
    {
        if (Effect == null) return;
        Effect.SetActive(true);
    }

    public void ActivateAdditionalEffect()
    {
        if (AdditionalEffect == null) return;
        AdditionalEffect.SetActive(true);
    }

    public void ActivateCharacterEffect()
    {
        if (CharacterEffect == null) return;
        CharacterEffect.SetActive(true);
    }

    public void ActivateCharacterEffect2()
    {
        if (CharacterEffect2 == null) return;
        CharacterEffect2.SetActive(true);
    }

    public void SetSpellPosition(Transform CharacterAttachPoint, Transform CharacterAttachPoint2,
        Transform AttachPoint, Transform AdditionalEffectAttachPoint)
    {

        if (Effect != null && AttachPoint != null)
        {
            Effect.transform.position = AttachPoint.position;
        }
        if (AdditionalEffect != null && AdditionalEffectAttachPoint != null)
        {
            AdditionalEffect.transform.position = AdditionalEffectAttachPoint.position;
        }
        if (CharacterEffect != null && CharacterAttachPoint != null)
        {
            CharacterEffect.transform.position = CharacterAttachPoint.position;
        }
        if (CharacterEffect2 != null && CharacterAttachPoint2 != null)
        {
            CharacterEffect2.transform.position = CharacterAttachPoint2.position;
        }
    }
}
