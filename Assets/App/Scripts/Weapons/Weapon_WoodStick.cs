using UnityEngine;
public class Weapon_WoodStick : Weapon
{
    //[Header("Settings")]

    public override void StartAttack(Vector3 lookDir)
    {
        if (!CanAttack()) return;
        base.StartAttack(lookDir);
    }
}