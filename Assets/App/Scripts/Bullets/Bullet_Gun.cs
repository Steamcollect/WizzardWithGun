using UnityEngine;

public class Bullet_Gun : Bullet
{
    [Header("Settings")]
    [SerializeField] float trailMoveSpeed;
    [SerializeField] float trailMoveRange;

    [Header("References")]
    [SerializeField] Transform trail;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Update()
    {
        float posX = Mathf.Cos(Time.time * trailMoveSpeed) * trailMoveRange;
        trail.localPosition = new Vector3(posX, trail.localPosition.y, trail.localPosition.z);
    }

    protected override void _OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
    }
}