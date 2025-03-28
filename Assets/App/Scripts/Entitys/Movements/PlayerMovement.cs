using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;

    Vector2 input;

    [Header("References")]
    [SerializeField] Rigidbody rb;
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
        anim.speed = Mathf.Clamp01(rb.linearVelocity.sqrMagnitude / maxSpeed);
    }

    void Move()
    {
        rb.AddForce(input.ToVector3() * moveSpeed);
    }

    public void SetInput(Vector2 input)
    {
        this.input = input;
    }
}