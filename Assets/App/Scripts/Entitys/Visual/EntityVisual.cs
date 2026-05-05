using MVsToolkit.Utilities;
using System;
using UnityEngine;

public class EntityVisual : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float verticalRange;
    [SerializeField] float verticalSpeed;

    [Space(5)]
    [SerializeField] float horizontalRange;
    [SerializeField] float horizontalSpeed;

    [Space(5)]
    [SerializeField] float rotationRange;
    [SerializeField] float rotationSpeed;

    Vector3 defaultPos, defaultRot;

    float animationSpeed;
    float time;

    [Header("References")]
    [SerializeField] Transform animatedObject;

    [SerializeField] EntityMovement movement;

    private void Start()
    {
        defaultPos = animatedObject.localPosition;
        defaultRot = animatedObject.localEulerAngles;

        time = UnityEngine.Random.value;

        movement.OnSpeedChange += SetAnimationSpeed;
    }

    private void Update()
    {
        time += Time.deltaTime;

        Animate();
    }

    public virtual void Animate()
    {
        if (animationSpeed <= .05f) return;

        animatedObject.localPosition = new Vector3
            (
                Mathf.Sin(time * verticalSpeed * animationSpeed) * verticalRange,
                Mathf.Sin(time * horizontalSpeed * animationSpeed) * horizontalRange,
                defaultPos.z
            );

        animatedObject.localEulerAngles = new Vector3
            (
                defaultRot.x,
                defaultRot.y,
                Mathf.Sin(time * rotationSpeed * animationSpeed) * rotationRange
            );
    }

    void SetAnimationSpeed(float speed)
    {
        print(speed);
        animationSpeed = speed;
    }
}