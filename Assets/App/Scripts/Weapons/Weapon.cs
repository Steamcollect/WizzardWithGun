using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float attackCooldown;
    [SerializeField] int attackDamage;

    bool isOnAttackCooldown = false;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    public Action<EntityMotor> onEntityTouch;

    public bool CanAttack()
    {
        return !isOnAttackCooldown && _CanAttack();
    }
    protected abstract bool _CanAttack();

    public abstract void Attack(Vector3 lookDir);

    protected IEnumerator AttackCooldown()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    public int GetAttackDamage() { return attackDamage; }
}