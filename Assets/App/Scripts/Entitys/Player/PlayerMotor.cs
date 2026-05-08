using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : EntityMotor
{
    //[Header("Settings")]
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
        rseSetCamTarget.Invoke(transform);
    }

    private void Update()
    {
        movement.SetInput(movementIA.action.ReadValue<Vector2>().normalized);
    }
}