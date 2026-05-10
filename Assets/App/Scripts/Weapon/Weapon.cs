using MVsToolkit.Attributes;
using MVsToolkit.Pool;
using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float attackCooldown;

    [SerializeField, ReadOnly] protected bool isAttacking = false;
    [SerializeField, ReadOnly] protected bool isOnAttackCooldown = false;

    Vector3 cameraLookDir;

    [Header("References")]
    [SerializeField] Transform content;

    MVsPool<Weapon> poolConnected;

    protected EntityStatistics statistics;

    public Action<EntityMotor> onDamageApplyToEntity;

    #region Exposed Methods
    public Weapon Initialize(MVsPool<Weapon> pool)
    {
        poolConnected = pool;
        return this;
    }

    public virtual Weapon StartHandle(EntityStatistics statistics)
    {
        this.statistics = statistics;
        return this;
    }

    public void RotateTowardCamera(Vector3 camDirection)
    {
        cameraLookDir = camDirection;
        content.forward = camDirection;
    }

    public virtual void Rotate(float angle)
    {
        transform.eulerAngles = new Vector3(0, -angle, 0);

        if (angle < -90 || angle > 90) content.localScale = new Vector3(1, -1, 1);
        else content.localScale = Vector3.one;

        content.rotation =
            Quaternion.LookRotation(cameraLookDir, Vector3.up)
            * Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void StartAttack(Vector3 lookDir)
    {
        isAttacking = true;
    }

    public virtual void CancelAttack(Vector3 lookDir) 
    {
        isAttacking = false;
    }

    /// <summary>
    /// Can the weapon currently attack, can be override if the current weapon require
    /// </summary>
    /// <returns></returns>
    public virtual bool CanAttack()
    {
        return !isOnAttackCooldown && !isAttacking;
    }

    public virtual void Destroy()
    {
        poolConnected.Release(this);
    }
    #endregion Exposed Methods

    #region Methods
    protected IEnumerator AttackCooldown()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }
    #endregion Methods
}