using UnityEngine;
public class Weapon_WoodStick : Weapon
{
    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]
    public override void Attack(Vector3 lookDir)
    {
        
    }

    protected override bool _CanAttack()
    {
        return false;
    }
}