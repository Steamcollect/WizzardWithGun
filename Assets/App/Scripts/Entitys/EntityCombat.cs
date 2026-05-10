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
    [SerializeField] protected RSF_GetWeapon rsfGetWeapon;

    public EntityCombat Initialize(EntityStatistics statistics)
    {
        this.statistics = statistics;
        SetWeapon(rsfGetWeapon.Invoke(spawningWeapon.GetRandomWeapon().Type));
        
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
        weapon.StartHandle(statistics);
    }
}