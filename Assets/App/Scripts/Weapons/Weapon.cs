using MVsToolkit.Attributes;
using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float attackCooldown;

    [SerializeField, ReadOnly] protected bool isAttacking = false;
    [SerializeField, ReadOnly] protected bool isOnAttackCooldown = false;

    public Action<EntityMotor> onDamageApplyToEntity;
    
    public virtual void StartAttack(Vector3 lookDir)
    {
        isAttacking = true;
    }

    public virtual void CancelAttack(Vector3 lookDir) 
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
}