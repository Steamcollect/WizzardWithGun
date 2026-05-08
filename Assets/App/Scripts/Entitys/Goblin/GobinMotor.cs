using UnityEngine;

public class GoblinMotor : EntityMotor
{
    [Header("Settings")]
    [SerializeField] float attackRange;

    //[Header("References")]

    [Space(10)]
    [SerializeField] RSO_PlayerTransform rsoPlayerTransform;

    //[Header("Input")]
    //[Header("Output")]

    private void Update()
    {
        Vector2 current = transform.position.ToVector2();
        Vector2 target = rsoPlayerTransform.Get().position.ToVector2();

        float distanceFromPlayer = Vector2.Distance(current, target);
        Vector2 lookDir = (target - current).normalized;

        if (distanceFromPlayer > attackRange)
        {
            movement.SetInput(lookDir);
        }
        else
        {
            movement.SetInput(Vector2.zero);
            //combat.Attack(lookDir);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}