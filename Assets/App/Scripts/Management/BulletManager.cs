using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] _Bullet[] bullets;
    Dictionary<string, Queue<Bullet>> bulletDictionary = new();

    [System.Serializable]
    struct _Bullet
    {
        public string bulletName;
        public Bullet bulletPrefab;

        [Space(5)]
        public int instantiateAmount;
    }

    [Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    [SerializeField] RSF_GetBullet rsfGetBullet;
    // RSP

    [Header("Input")]
    [SerializeField] RSE_ReturnBullet rseReturnBullet;

    //[Header("Output")]

    private void OnEnable()
    {
        rsfGetBullet.AddListener(GetBullet);
        rseReturnBullet.AddListener(ReturnBullet);
    }
    private void OnDisable()
    {
        rsfGetBullet.RemoveListener(GetBullet);
        rseReturnBullet.RemoveListener(ReturnBullet);
    }

    private void Start()
    {
        foreach (_Bullet _bullet in bullets)
        {
            for (int i = 0; i < _bullet.instantiateAmount; i++)
            {
                CreateBullet(_bullet.bulletName);
            }
        }
    }

    Bullet GetBullet(string bulletName)
    {
        if (!bulletDictionary.ContainsKey(bulletName) || bulletDictionary[bulletName].Count <= 0)
            CreateBullet(bulletName);

        Bullet bullet = bulletDictionary[bulletName].Dequeue();
        bullet.gameObject.SetActive(true);
        return bullet;
    }
    void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletDictionary[bullet.GetName()].Enqueue(bullet);
    }

    void CreateBullet(string bulletName)
    {
        _Bullet? _bullet = bullets.FirstOrDefault(x => x.bulletName == bulletName);

        if(_bullet == null)
        {
            Debug.LogError($"There is no \"{bulletName}\" in the Bullet Manager");
            return;
        }

        if (!bulletDictionary.ContainsKey(bulletName))
        {
            bulletDictionary.Add(bulletName, new Queue<Bullet>());
        }

        Bullet bullet = Instantiate(_bullet.Value.bulletPrefab, transform);
        bullet.SetName(bulletName);
        bullet.gameObject.SetActive(false);

        bulletDictionary[bulletName].Enqueue(bullet);
    }
}