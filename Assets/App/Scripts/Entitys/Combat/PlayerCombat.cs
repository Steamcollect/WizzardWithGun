using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : EntityCombat
{
    //[Header("Settings")]
    Vector3 lookDir;

    [Header("References")]
    [SerializeField] Transform weaponContent;

    [SerializeField] Weapon currentWeapon;

    [Space(10)]
    [SerializeField] InputActionReference mousePositionIA;
    [SerializeField] InputActionReference attackIA;

    [Space(5)]
    [SerializeField] RSO_PlayerCombatLookDir rsoPlayerCombatLookDir;

    private void OnEnable()
    {
        mousePositionIA.action.Enable();
        attackIA.action.Enable();

        attackIA.action.started += AttackInputStart;
        attackIA.action.canceled += AttackInputRelease;
    }
    private void OnDisable()
    {
        attackIA.action.started -= AttackInputStart;
        attackIA.action.canceled -= AttackInputRelease;
    }

    private void Update()
    {
        RotateWeapon();
    }

    public override void Setup(SSO_EntityData data)
    {
        SetWeapon(currentWeapon);
    }

    void AttackInputStart(InputAction.CallbackContext ctx)
    {
        currentWeapon.StartAttack(lookDir);
    }
    
    void AttackInputRelease(InputAction.CallbackContext ctx)
    {
        currentWeapon.ReleaseAttack(lookDir);
    }

    void RotateWeapon()
    {
        if (GetMousePositionOnGround(out Vector3 pos))
        {
            lookDir = (pos - transform.position).normalized;
            rsoPlayerCombatLookDir.Value = lookDir;

            float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg;

            if (angle < -90 || angle > 90) weaponContent.localScale = new Vector3(1, -1, 1);
            else weaponContent.localScale = Vector3.one;

            if (angle > 0) weaponContent.localPosition = new Vector3(0, 0, .1f);
            else weaponContent.localPosition = new Vector3(0, 0, -.1f);

            weaponContent.localRotation = Quaternion.Euler(weaponContent.localRotation.eulerAngles.x, weaponContent.localRotation.eulerAngles.y, angle);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        weaponContent.forward = -rsoCameraDirection.Value;
        weapon.onEntityTouch += OnEntityTouch;
    }

    void OnEntityTouch(EntityMotor entity)
    {
        entity.GetHealth().TakeDamage(attackDamage + currentWeapon.GetAttackDamage());
    }

    bool GetMousePositionOnGround(out Vector3 hitPosition)
    {
        hitPosition = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(mousePositionIA.action.ReadValue<Vector2>());
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            hitPosition = ray.GetPoint(distance);
            return true;
        }

        return false;
    }
}