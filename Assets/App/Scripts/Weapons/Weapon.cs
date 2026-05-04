using MVsToolkit.Attributes;
using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float attackCooldown;
    [SerializeField] int attackDamage;

    [SerializeField, ReadOnly] protected bool isAttacking = false;

    bool isOnAttackCooldown = false;

    /// <summary>
    /// On weapon apply damage to something
    /// </summary>
    public Action<EntityMotor> onEntityTouch;
    
    public virtual void StartAttack(Vector3 lookDir)
    {
        isAttacking = true;
    }

    public virtual void ReleaseAttack(Vector3 lookDir) 
    {
        isAttacking = false;
    }

    protected IEnumerator AttackCooldown()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    /// <summary>
    /// Can the weapon currently attack, can be override if the current weapon require
    /// </summary>
    /// <returns></returns>
    public virtual bool CanAttack()
    {
        return !isOnAttackCooldown && !isAttacking;
    }

    public int GetAttackDamage() { return attackDamage; }
}