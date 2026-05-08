using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : EntityCombat
{
    //[Header("Settings")]
    Vector3 lookDir;

    [Header("References")]

    [Space(10)]
    [SerializeField] InputActionReference mousePositionIA;
    [SerializeField] InputActionReference attackIA;

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

    void AttackInputStart(InputAction.CallbackContext ctx)
    {
        currentWeapon.StartAttack(lookDir);
    }
    
    void AttackInputRelease(InputAction.CallbackContext ctx)
    {
        currentWeapon.CancelAttack(lookDir);
    }

    void RotateWeapon()
    {
        if (GetMousePositionOnGround(out Vector3 pos))
        {
            lookDir = (pos - transform.position).normalized;

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