using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] PlayerMovement movement;

    [Space(10)]
    // RSO
    [SerializeField] RSO_PlayerTransform rsoPlayerTransform;

    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_SetCameraTarget rseSetCamTarget;

    private void Awake()
    {
        rsoPlayerTransform.Value = transform;
    }
    private void Start()
    {
        rseSetCamTarget.Call(transform);
    }

    private void Update()
    {
        movement.SetInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized);
    }
}