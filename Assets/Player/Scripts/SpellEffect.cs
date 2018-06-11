#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject CharacterEffect;
    [SerializeField]
    private GameObject CharacterEffect2;
    [SerializeField]
    private GameObject Effect;
    [SerializeField]
    private GameObject AdditionalEffect;
    
    [SerializeField]
    public GameObject StaticPartEffect;
    [HideInInspector]
    public Vector3 staticEffectPositionRelationPlayer;
    [HideInInspector]
    public Quaternion staticEffectRotationRelationPlayer;

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

    public void UpdateSpellPosition(Transform CharacterAttachPoint, Transform CharacterAttachPoint2,
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
        //Debug.LogWarning(StaticPartEffect.transform.localRotation);
    }

    public void SetupSpellPosition(Transform playerTransform)
    {
        if (StaticPartEffect != null)
        {
            gameObject.transform.SetParent(playerTransform);
            StaticPartEffect.transform.localPosition = staticEffectPositionRelationPlayer;
            StaticPartEffect.transform.localRotation = staticEffectRotationRelationPlayer;
            gameObject.transform.parent = null;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SpellEffect))]
public class SpellEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var spell = target as SpellEffect;

        using (new EditorGUI.DisabledScope(spell.StaticPartEffect==null))
        {
            spell.staticEffectPositionRelationPlayer = EditorGUILayout.
                Vector3Field("Position Relation Player", spell.staticEffectPositionRelationPlayer);
            spell.staticEffectRotationRelationPlayer = Vector4ToQuaternion(
                EditorGUILayout.Vector4Field("Rotation Relation Player",
                QuaternionToVector4(spell.staticEffectRotationRelationPlayer)));

        }
    }

    static Vector4 QuaternionToVector4(Quaternion rot)
    {
        return new Vector4(rot.x, rot.y, rot.z, rot.w);
    }

    static Quaternion Vector4ToQuaternion(Vector4 rot)
    {
        return new Quaternion(rot.x, rot.y, rot.z, rot.w);
    }
}
#endif