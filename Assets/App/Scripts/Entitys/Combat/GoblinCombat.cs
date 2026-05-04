using UnityEngine;

public class GoblinCombat : EntityCombat
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] Transform weaponContent;

    [SerializeField] Weapon weapon;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    public override void Setup(SSO_EntityData data)
    {
        SetWeapon(weapon);
    }

    public void SetWeapon(Weapon weapon)
    {
        weaponContent.forward = -rsoCameraDirection.Value;
        weapon.onEntityTouch += OnEntityTouch;
    }

    void OnEntityTouch(EntityMotor entity)
    {
        entity.GetHealth().TakeDamage(attackDamage + weapon.GetAttackDamage());
    }
}