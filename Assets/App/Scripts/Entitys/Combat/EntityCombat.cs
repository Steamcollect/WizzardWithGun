using UnityEngine;

public abstract class EntityCombat : MonoBehaviour
{
    [Header("Main Settings")]
    protected int attackDamage;

    [Header("Main References")]
    [SerializeField] protected RSO_CameraDirection rsoCameraDirection;

    private void Start()
    {
        Setup(0);
    }

    public void Setup(int attackDamage)
    {
        this.attackDamage = attackDamage;
        OnSetup();
    }
    public abstract void OnSetup();

    public abstract void Attack(Vector3 lookDir);
}