using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    //[Header("Settings")]
    float moveSpeed;
    Vector3 moveDir;

    string bulletName;

    [Header("References")]
    [SerializeField] Rigidbody rb;

    [Space(10)]
    [SerializeField] SSO_GameplayConfig ssoGameplayConfig;

    // RSO
    [SerializeField] RSO_CameraDirection rsoCameraDirection;
    [SerializeField] RSO_PlayerTransform rsoPlayerTransform;
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_ReturnBullet rseReturnbullet;
    
    protected Action<Bullet, Collider> onTriggerEnter;

    private void Awake()
    {
        transform.forward = -rsoCameraDirection.Value;
    }

    public void Setup(Vector3 moveDir, float moveSpeed, Action<Bullet, Collider> onTouchSomething)
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
        if (Vector3.Distance(transform.position, rsoPlayerTransform.Value.position) 
            > ssoGameplayConfig.maxBulletDistanceFromPlayer)
        {
            ReturnToQueue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(this, other);
        _OnTriggerEnter(other);
    }
    protected abstract void _OnTriggerEnter(Collider other);

    public void SetName(string name) { bulletName = name; }
    public string GetName() { return bulletName; }
    public void ReturnToQueue()
    {
        rseReturnbullet.Call(this);
    }
}