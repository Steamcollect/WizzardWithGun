using System;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;
    Vector2 input;

    [Header("References")]
    [SerializeField] protected Rigidbody rb;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]
    
    /// <summary>
    /// Call on FixedUpdate, return clamp01 float base on speed on maxSpeed
    /// </summary>
    public Action<float> OnSpeedChange;

    private void FixedUpdate()
    {
        Move();
    }
    //protected abstract void OnFixedUpdate();

    void Move()
    {
        rb.AddForce(input.ToVector3() * moveSpeed);

        float speedOnOne = rb.linearVelocity.sqrMagnitude;
        OnSpeedChange?.Invoke(Mathf.Clamp01(rb.linearVelocity.sqrMagnitude / maxSpeed));
    }

    public void SetInput(Vector2 input)
    {
        this.input = input;
    }
}