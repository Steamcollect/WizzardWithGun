using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float distanceFromTarget;
    [SerializeField] float timeOffset;

    Vector3 posOffset;
    Vector3 velocity;

    public static Vector3 LookDirection;

    //[Header("References")]
    Transform target;

    static CameraController instance;

    #region Unity Methods
    private void Awake()
    {
        LookDirection = transform.position.normalized;

        if(instance != null) Destroy(gameObject);
        else instance = this;
    }

    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + posOffset, ref velocity, timeOffset);
        }
    }

    void OnValidate()
    {
        Vector3 posDir = transform.position.normalized;

        if (distanceFromTarget <= 0) distanceFromTarget = .1f;
        posOffset = posDir * distanceFromTarget;
        transform.position = posOffset;
    }
    #endregion Unity Methods

    #region Exposed Methods
    public static void SetTarget(Transform target)
    {
        if (instance != null) instance._SetTarget(target);
    }

    public static void Shock(float force)
    {
        if (instance != null) instance._Shock(force);
    }
    #endregion Exposed Methods

    #region Methods
    void _SetTarget(Transform target)
    {
        this.target = target;
    }

    void _Shock(float force)
    {
        Vector3 pos = Random.onUnitSphere * force;
        transform.position += pos;
    }
    #endregion Methods    
}