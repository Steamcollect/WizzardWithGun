using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : EntityController
{
    //[Header("Settings")]
    [Header("References")]
    [SerializeField] InputActionReference movementIA;

    [Space(10)]
    [SerializeField] RSO_PlayerTransform rsoPlayerTransform;

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
        CameraController.SetTarget(transform);
    }

    private void Update()
    {
        movement.SetInput(movementIA.action.ReadValue<Vector2>().normalized);
    }
}