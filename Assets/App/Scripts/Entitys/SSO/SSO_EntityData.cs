using UnityEditor.U2D.Sprites;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_EntityData", menuName = "SSO/Entity/SSO_EntityData")]
public class SSO_EntityData : ScriptableObject
{
    public int Health;
    public int Damage;
    public float MoveSpeed;

    [Space]
    public SSO_EntitySpawningWeapon SpawningWeapon;
}