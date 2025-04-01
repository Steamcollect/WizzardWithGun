using UnityEngine;
public class EntityMotor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float ySpawnOffset;

    string entityName;

    [Header("References")]
    [SerializeField] SSO_EntityData entityData;

    [Space(5)]
    [SerializeField] protected EntityHealth health;
    [SerializeField] protected EntityCombat combat;
    [SerializeField] protected EntityMovement movement;

    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_ReturnEntity rseReturnEntity;

    private void Awake()
    {
        health.onDeath += ReturnToQueue;
    }

    public void Setup()
    {
        combat.Setup(entityData.attackDamage);
        health.Setup(entityData.health);
    }

    public void SetName(string name)
    {
        entityName = name;
    }
    public string GetName() { return entityName; }
    public void ReturnToQueue()
    {
        rseReturnEntity.Call(this);
    }

    public float GetYSpawnOffset() { return ySpawnOffset; }

    public EntityHealth GetHealth() { return health; }
    public EntityCombat GetCombat() { return combat; }
    public EntityMovement GetMovement() {  return movement; }
}