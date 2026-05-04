using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : EntityMotor
{
    //[Header("Settings")]
    Vector2 movementInput;

    [Header("References")]
    [SerializeField] InputActionReference movementIA;

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

    private void OnEnable()
    {
        movementIA.action.Enable();
    }

    protected override void Start()
    {
        base.Start();
        rseSetCamTarget.Call(transform);
    }

    private void Update()
    {
        movementInput = movementIA.action.ReadValue<Vector2>().normalized;

        movement.SetInput(movementInput);
    }
}