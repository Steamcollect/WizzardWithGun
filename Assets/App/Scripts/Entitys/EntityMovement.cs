using System;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [Header("Settings")]
    Vector2 input;

    [Header("References")]
    protected EntityStatistics statistics;
    [SerializeField] protected Rigidbody rb;
    
    /// <summary>
    /// Call on FixedUpdate, return clamp01 float base on speed on maxSpeed
    /// </summary>
    public Action<float> OnSpeedChange;

    private void FixedUpdate()
    {
        Move();
    }

    public virtual EntityMovement Initialize(EntityStatistics statistics)
    {
        this.statistics = statistics;
        return this;
    }

    void Move()
    {
        rb.AddForce(input.ToVector3() * statistics.MoveSpeed);

        float speedOnOne = rb.linearVelocity.sqrMagnitude;
        OnSpeedChange?.Invoke(Mathf.Clamp01(rb.linearVelocity.sqrMagnitude / statistics.MoveSpeed));
    }

    public void SetInput(Vector2 input)
    {
        this.input = input;
    }
}