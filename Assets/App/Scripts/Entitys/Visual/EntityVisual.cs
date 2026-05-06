using UnityEngine;

public class EntityVisual : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float verticalRange;
    [SerializeField] float horizontalRange;
    [SerializeField] float rotationRange;

    [Space]
    [SerializeField] float animationSpeed;

    Vector3 defaultPos, defaultRot;

    float animationSpeedMult;
    float time;

    [Header("References")]
    [SerializeField] Transform animatedObject;

    [SerializeField] EntityMovement movement;

    private void Start()
    {
        defaultPos = animatedObject.localPosition;
        defaultRot = animatedObject.localEulerAngles;

        time = Random.value;

        movement.OnSpeedChange += SetAnimationSpeed;
    }

    private void Update()
    {
        time += Time.deltaTime * animationSpeedMult;
        Animate();
    }

    public virtual void Animate()
    {
        if (animationSpeedMult <= .05f) return;

        animatedObject.localPosition = new Vector3
            (
                Mathf.Sin(time * animationSpeed) * verticalRange,
                horizontalRange + Mathf.Sin((time +.5f) * animationSpeed * 2) * horizontalRange,
                defaultPos.z
            );

        animatedObject.localEulerAngles = new Vector3
            (
                defaultRot.x,
                defaultRot.y,
                Mathf.Sin(time * animationSpeed) * rotationRange
            );
    }

    void SetAnimationSpeed(float speed)
    {
        animationSpeedMult = speed;
    }
}