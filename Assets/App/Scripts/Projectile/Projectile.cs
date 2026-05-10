using MVsToolkit.Pool;
using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    //[Header("Settings")]
    float moveSpeed;
    Vector3 moveDir;

    [Header("References")]
    [SerializeField] Rigidbody rb;

    [Space(10)]
    [SerializeField] SSO_GameplayConfig ssoGameplayConfig;

    [Space(5)]
    [SerializeField] RSO_CameraDirection rsoCameraDirection;
    [SerializeField] RSO_PlayerTransform rsoPlayerTransform;

    protected Action<Projectile, Collider> onTriggerEnter;

    MVsPool<Projectile> poolConnected;

    private void Awake()
    {
        transform.forward = -rsoCameraDirection.Value;
    }

    public void Initialize(MVsPool<Projectile> pool)
    {
        poolConnected = pool;
    }

    public void ShootSetup(Vector3 moveDir, float moveSpeed, Action<Projectile, Collider> onTouchSomething)
    {
        this.moveDir = new Vector3(moveDir.x, 0, moveDir.z);

        float angle = Mathf.Atan2(moveDir.z, moveDir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);

        this.moveSpeed = moveSpeed;

        onTriggerEnter += onTouchSomething;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * moveSpeed;

        CheckPlayerDistance();
    }

    protected virtual void CheckPlayerDistance()
    {
        if (Vector3.Distance(transform.position, rsoPlayerTransform.Value.position)
            > ssoGameplayConfig.maxBulletDistanceFromPlayer)
        {
            Destroy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(this, other);
        _OnTriggerEnter(other);
    }
    protected virtual void _OnTriggerEnter(Collider other) { }

    public void Destroy()
    {
        poolConnected.Release(this);
    }
}