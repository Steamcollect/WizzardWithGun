using UnityEngine;

public class GoblinMotor : EntityMotor
{
    [Header("Settings")]
    [SerializeField] float attackRange;

    [Header("References")]
    [SerializeField] GoblinMovement movement;

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
            movement.SetInput((rsoPlayerTransform.Value.position - transform.position).ToVector2().normalized);
        else
            movement.SetInput(Vector2.zero);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}