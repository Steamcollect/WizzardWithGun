using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float distanceFromTarget;
    Vector3 posOffset;

    [SerializeField] float timeOffset;

    Vector3 velocity;

    //[Header("References")]
    Transform target;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_SetCameraTarget rseSetCamTarget;
    [SerializeField] RSE_CameraShoke rseCameraShoke;

    //[Header("Output")]

    private void OnEnable()
    {
        rseSetCamTarget.AddListener(SetTarget);
        rseCameraShoke.AddListener(Shoke);
    }
    private void OnDisable()
    {
        rseSetCamTarget.RemoveListener(SetTarget);
        rseCameraShoke.RemoveListener(Shoke);
    }

    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + posOffset, ref velocity, timeOffset);
        }
    }

    void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Shoke(float range)
    {
        Vector3 pos = Random.onUnitSphere * range;
        transform.position += pos;
    }

    private void OnValidate()
    {
        Vector3 posDir = transform.position.normalized;

        if (distanceFromTarget <= 0) distanceFromTarget = .1f;
        posOffset = posDir * distanceFromTarget;
        transform.position = posOffset;
    }
}