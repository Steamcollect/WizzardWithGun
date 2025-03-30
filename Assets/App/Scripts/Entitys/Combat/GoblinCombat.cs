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

    private void Start()
    {
        SetWeapon(weapon);
    }

    public override void Attack(Vector3 lookDir)
    {
        if (weapon.CanAttack())
        {
            weapon.Attack(lookDir);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        weaponContent.LookAt(-cam.transform.position);
        weapon.onEntityTouch += OnEntityTouch;
    }

    void OnEntityTouch(EntityMotor entity)
    {
        entity.GetHealth().TakeDamage(attackDamage + weapon.GetAttackDamage());
    }
}