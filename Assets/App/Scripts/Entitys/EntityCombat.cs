using UnityEngine;

public abstract class EntityCombat : MonoBehaviour
{
    [Header("Main References")]
    protected Weapon weapon;
    protected EntityStatistics statistics;
    
    [Space]
    [SerializeField] protected RSF_GetWeapon rsfGetWeapon;

    public EntityCombat Initialize(EntityStatistics statistics)
    {
        this.statistics = statistics;
        HandleWeapon(rsfGetWeapon.Invoke(statistics.SpawningWeapon.GetRandomWeapon().Type));
        
        return this;
    }

    public virtual void HandleWeapon(Weapon newWeapon)
    {
        if(weapon != null)
        {
            weapon.Destroy();
        }

        weapon = newWeapon;

        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        weapon.RotateTowardCamera(-CameraController.LookDirection);
        weapon.StartHandle(statistics);
    }
}