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
        this.statistics = statistics;

        SetWeapon(spawningWeapon.GetRandomWeapon().prefab);
        
        return this;
    }

    public virtual void SetWeapon(Weapon weaponPrefab)
    {
        if(weapon != null)
        {
            weapon.Destroy();
        }

        weapon = Instantiate(weaponPrefab, transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        weapon.RotateTowardCamera(-rsoCameraDirection.Value);
        weapon.Initialize(statistics);
    }
}