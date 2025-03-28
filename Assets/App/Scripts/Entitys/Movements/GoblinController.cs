using UnityEngine;
public class GoblinController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;
    Vector3 velocity;

    [Space(5)]
    [SerializeField] float attackRange;

    [Space(5)]
    [SerializeField] float maxSpeed;

    [Header("References")]
    [SerializeField] Animator anim;

    [Space(10)]
    // RSO
    [SerializeField] RSO_PlayerTransform rsoPlayerTransform;

    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, rsoPlayerTransform.Value.position);

        if (distanceFromPlayer > attackRange)
        {
            transform.position = Vector3.SmoothDamp(transform.position, rsoPlayerTransform.Value.position, ref velocity, distanceFromPlayer / moveSpeed);
        }
        else velocity = Vector3.zero;

        anim.speed = Mathf.Clamp01(velocity.sqrMagnitude / maxSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}