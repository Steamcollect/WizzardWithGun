using UnityEngine;
public class EntityMotor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float ySpawnOffset;

    string entityName;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_ReturnEntity rseReturnEntity;

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
}