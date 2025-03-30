using UnityEngine;

public abstract class EntityCombat : MonoBehaviour
{
    [Header("Main Settings")]
    protected int attackDamage;

    protected Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void Setup(int attackDamage)
    {
        this.attackDamage = attackDamage;
    }

    public abstract void Attack(Vector3 lookDir);
}