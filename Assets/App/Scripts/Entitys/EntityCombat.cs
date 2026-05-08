using UnityEngine;

public abstract class EntityCombat : MonoBehaviour
{
    [Header("Main References")]
    [SerializeField] protected Transform weaponContent;
    [SerializeField] protected Weapon currentWeapon;

    protected EntityStatistics statistics;

    [Space]
    [SerializeField] protected RSO_CameraDirection rsoCameraDirection;

    public EntityCombat Initialize(EntityStatistics statistics)
    {
        this.statistics = statistics;
        return this;
    }

    public virtual void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}