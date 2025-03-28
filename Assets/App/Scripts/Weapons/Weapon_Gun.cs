using System.Collections;
using UnityEngine;

public class Weapon_Gun : Weapon
{
    [Header("Settings")]
    [SerializeField] float musleFlashDelay;
    [SerializeField] float camShokeRange;

    [Header("References")]
    [SerializeField] GameObject musleFlashGO;
    [SerializeField] Animator anim;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_CameraShoke rseCamShoke;    

    public override void Attack()
    {
        StartCoroutine(AttackCooldown());

        anim.SetTrigger("Shoot");

        if (musleFlashCoroutine != null) StopCoroutine(musleFlashCoroutine);
        StartCoroutine(MusleFlashDelay());

        rseCamShoke.Call(camShokeRange);
    }

    Coroutine musleFlashCoroutine;
    IEnumerator MusleFlashDelay()
    {
        musleFlashGO.SetActive(true);
        yield return new WaitForSeconds(musleFlashDelay);
        musleFlashGO.SetActive(false);
    }

    protected override bool _CanAttack()
    {
        return true;
    }
}