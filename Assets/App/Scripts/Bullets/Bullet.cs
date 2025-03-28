using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    //[Header("Settings")]
    float moveSpeed;
    Vector3 moveDir;

    [Header("References")]
    [SerializeField] Rigidbody rb;

    [Space(10)]
    // RSO
    [SerializeField] RSO_CameraDirection rsoCameraDirection;
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]
    public Action<Bullet, Collider> onTriggerEnter;

    private void Awake()
    {
        transform.forward = -rsoCameraDirection.Value;
    }

    public void Setup(Vector3 moveDir, float moveSpeed)
    {
        this.moveDir = new Vector3(moveDir.x, 0, moveDir.z);

        float angle = Mathf.Atan2(moveDir.z, moveDir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);

        this.moveSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(this, other);
        _OnTriggerEnter(other);
    }
    protected abstract void _OnTriggerEnter(Collider other);
}