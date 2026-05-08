using UnityEngine;

public class GoblinCombat : EntityCombat
{
    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    public override void SetWeapon(Weapon weapon)
    {
        weaponContent.forward = -rsoCameraDirection.Value;
    }
}