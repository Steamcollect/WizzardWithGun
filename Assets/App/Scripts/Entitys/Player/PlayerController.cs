using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : EntityController
{
    //[Header("Settings")]
    public static Transform Transform;

    [Header("References")]
    [SerializeField] InputActionReference movementIA;

    private void Awake()
    {
        Transform = transform;
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