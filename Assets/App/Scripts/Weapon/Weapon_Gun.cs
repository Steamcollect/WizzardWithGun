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
    [SerializeField] ProjectileType projectileType;
    [SerializeField] Transform bulletSpawnPoint;

    [Space(10)]
    [SerializeField] GameObject musleFlashGO;
    [SerializeField] Animator anim;
    
    Coroutine musleFlashCoroutine;

    [Header("Output")]
    [SerializeField] RSF_GetProjectile rsfGetBullet;

    #region Exposed Methods
    public override void StartAttack(Vector3 lookDir)
    {
        if (!CanAttack()) return;
        base.StartAttack(lookDir);

        float posY = bulletSpawnPoint.position.y;
        if(posY < .3f) posY = .3f;
        else if(posY > 1.3f) posY = 1.3f;

        Projectile bullet = rsfGetBullet.Invoke(projectileType);
        bullet.transform.position = new Vector3(bulletSpawnPoint.position.x, posY, bulletSpawnPoint.position.z);

        bullet.ShootSetup(lookDir, bulletSpeed, OnBulletTouchSomething);

        StartCoroutine(AttackCooldown());

        anim.SetTrigger("Shoot");

        if (musleFlashCoroutine != null) StopCoroutine(musleFlashCoroutine);
        StartCoroutine(MusleFlashAnimation());

        CameraController.Shoke(camShokeRange);
    }
    #endregion Exposed Methods

    #region Methods
    void OnBulletTouchSomething(Projectile bullet, Collider touch)
    {
        if(touch.TryGetComponent(out EntityController entity))
        {
            onDamageApplyToEntity?.Invoke(entity);
        }

        bullet.Destroy();
    }

    IEnumerator MusleFlashAnimation()
    {
        musleFlashGO.SetActive(true);
        yield return new WaitForSeconds(musleFlashDelay);
        musleFlashGO.SetActive(false);
    }
    #endregion Methods
}