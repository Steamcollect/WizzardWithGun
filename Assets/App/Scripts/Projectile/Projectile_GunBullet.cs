using UnityEngine;

public class Projectile_GunBullet : Projectile
{
    [Header("Settings")]
    [SerializeField] float trailMoveSpeed;
    [SerializeField] float trailMoveRange;

    [Header("References")]
    [SerializeField] TrailRenderer trail;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void OnEnable()
    {
        trail.ResetBounds();
    }

    private void Update()
    {
        float posX = Mathf.Cos(Time.time * trailMoveSpeed) * trailMoveRange;
        trail.transform.localPosition = new Vector3(posX, trail.transform.localPosition.y, trail.transform.localPosition.z);
    }
}