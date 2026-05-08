using UnityEngine;

public abstract class EntityCombat : MonoBehaviour
{
    [Header("Main References")]

    [SerializeField] protected Transform weaponContent;
    [SerializeField] protected Weapon currentWeapon;

    [Space]
    [SerializeField] protected RSO_CameraDirection rsoCameraDirection;

    public virtual void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}