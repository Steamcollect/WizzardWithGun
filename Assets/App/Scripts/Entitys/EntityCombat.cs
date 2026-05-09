using MVsToolkit.Attributes;
using UnityEngine;

public abstract class EntityCombat : MonoBehaviour
{
    [Header("Main References")]
    protected Weapon weapon;
    protected EntityStatistics statistics;

    [Space]
    [SerializeField] protected SSO_EntitySpawningWeapon spawningWeapon;

    [Space]
    [SerializeField] protected RSO_CameraDirection rsoCameraDirection;

    public EntityCombat Initialize(EntityStatistics statistics)
    {
        InitializeFirstWeapon();
        this.statistics = statistics;
        return this;
    }

    void InitializeFirstWeapon()
    {
        SSO_EntitySpawningWeapon.EntitySpawningWeapon weapon = spawningWeapon.GetRandomWeapon();
        this.weapon = Instantiate(weapon.prefab, transform);
        this.weapon.transform.localPosition = Vector3.zero;
        this.weapon.transform.localRotation = Quaternion.identity;
    }

    public virtual void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
}