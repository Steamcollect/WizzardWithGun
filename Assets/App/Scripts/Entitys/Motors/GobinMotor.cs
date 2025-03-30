using UnityEngine;

public class GoblinMotor : EntityMotor
{
    [Header("Settings")]
    [SerializeField] float attackRange;

    //[Header("References")]

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
        Vector3 lookDir = (rsoPlayerTransform.Value.position - transform.position).normalized;

        if (distanceFromPlayer > attackRange)
        {
            movement.SetInput(lookDir.ToVector2());
        }
        else
        {
            movement.SetInput(Vector2.zero);
            combat.Attack(lookDir);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}