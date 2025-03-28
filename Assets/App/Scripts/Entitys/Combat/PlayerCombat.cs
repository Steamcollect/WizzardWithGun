using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //[Header("Settings")]
    Vector3 lookDir;

    [Header("References")]
    [SerializeField] Transform weaponContent;

    [SerializeField] Weapon currentWeapon;

    Camera cam;

    [Space(10)]
    // RSO
    [SerializeField] RSO_PlayerCombatLookDir rsoPlayerCombatLookDir;
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Start()
    {
        cam = Camera.main;
        weaponContent.LookAt(-cam.transform.position);
    }

    private void Update()
    {
        RotateWeapon();

        if (Input.GetKey(KeyCode.Mouse0) && currentWeapon.CanAttack())
        {
            currentWeapon.Attack(lookDir);
        }
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

    public static bool GetMousePositionOnGround(out Vector3 hitPosition)
    {
        hitPosition = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            hitPosition = ray.GetPoint(distance);
            return true;
        }

        return false;
    }
}