using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;
    Vector2 input;

    [Header("References")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] Animator anim;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void FixedUpdate()
    {
        Move();
    }
    //protected abstract void OnFixedUpdate();

    void Move()
    {
        rb.AddForce(input.ToVector3() * moveSpeed);
        anim.speed = Mathf.Clamp01(rb.linearVelocity.sqrMagnitude / maxSpeed);
    }

    public void SetInput(Vector2 input)
    {
        this.input = input;
    }
}