using UnityEngine;

public class Bullet_Gun : Bullet
{
    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    protected override void _OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}