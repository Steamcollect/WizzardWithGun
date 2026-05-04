using UnityEngine;

public abstract class EntityCombat : MonoBehaviour
{
    [Header("Main Settings")]
    protected int attackDamage;

    [Header("Main References")]
    [SerializeField] protected RSO_CameraDirection rsoCameraDirection;

    public virtual void Setup(SSO_EntityData data) {}
}