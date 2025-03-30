using System.Collections;
using UnityEngine;

public class Weapon_Gun : Weapon
{
    [Header("Settings")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float knockback;

    [Space(5)]
    [SerializeField] float musleFlashDelay;
    [SerializeField] float camShokeRange;

    [Header("References")]
    [SerializeField] string bulletName;
    [SerializeField] Transform bulletSpawnPoint;

    [Space(10)]
    [SerializeField] GameObject musleFlashGO;
    [SerializeField] Animator anim;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_CameraShoke rseCamShoke;
    [SerializeField] RSF_GetBullet rsfGetBullet;

    public override void Attack(Vector3 lookDir)
    {
        float posY = bulletSpawnPoint.position.y;
        if(posY < .3f) posY = .3f;
        else if(posY > 1.3f) posY = 1.3f;

        Bullet bullet = rsfGetBullet.Call(bulletName);
        bullet.transform.position = new Vector3(bulletSpawnPoint.position.x, posY, bulletSpawnPoint.position.z);

        bullet.Setup(lookDir, bulletSpeed, OnBulletTouchSomething);

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

    void OnBulletTouchSomething(Bullet bullet, Collider touch)
    {
        if(touch.TryGetComponent(out EntityHealth entityHealth))
        {
            onEntityTouch?.Invoke(entityHealth);
        }

        print("return to queue");
        bullet.ReturnToQueue();
    }
}