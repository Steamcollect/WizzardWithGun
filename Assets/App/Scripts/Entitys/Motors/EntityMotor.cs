using UnityEngine;
public class EntityMotor : MonoBehaviour
{
    //[Header("Settings")]
    string entityName;

    [Header("References")]
    [SerializeField] SSO_EntityData data;

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

    protected virtual void Start()
    {
        Setup();
    }

    public void Setup()
    {
        combat.Setup(data);
        health.Setup(data);
    }

    public void SetName(string name)
    {
        entityName = name;
    }
    public string GetName() { return entityName; }
    public void ReturnToQueue()
    {
        rseReturnEntity.Invoke(this);
    }

    public EntityHealth GetHealth() { return health; }
    public EntityCombat GetCombat() { return combat; }
    public EntityMovement GetMovement() {  return movement; }
}