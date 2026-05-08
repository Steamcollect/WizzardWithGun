using UnityEngine;
public class EntityMotor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] EntityStatistics statistics;

    [Space(5)]
    [SerializeField] protected EntityHealth health;
    [SerializeField] protected EntityCombat combat;
    [SerializeField] protected EntityMovement movement;

    //[Header("Input")]
    //[Header("Output")]

    protected virtual void Start()
    {
        Setup();
    }

    public void Setup()
    {
        health.Initialize(statistics);
        movement.Initialize(statistics);
        combat.Initialize(statistics);
    }

    public EntityHealth GetHealth() { return health; }
    public EntityCombat GetCombat() { return combat; }
    public EntityMovement GetMovement() {  return movement; }
}