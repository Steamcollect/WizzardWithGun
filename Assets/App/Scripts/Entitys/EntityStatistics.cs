using MVsToolkit.Attributes;
using UnityEngine;
public class EntityStatistics : MonoBehaviour
{
    [SerializeField] SSO_EntityData data;

    [Space]
    [ReadOnly] public int Health;
    [ReadOnly] public float MoveSpeed;
    [ReadOnly] public int Damage;

    private void Awake()
    {
        Health = data.Health;
        MoveSpeed = data.MoveSpeed;
        Damage = data.Damage;
    }

    public SSO_EntityData GetData() => data;
}