using UnityEngine;

[CreateAssetMenu(fileName = "SSO_GameplayConfig", menuName = "SSO/Game/SSO_GameplayConfig")]
public class SSO_GameplayConfig : ScriptableObject
{
    public float entitySpawningRange;

    [Space(5)]
    public float maxBulletDistanceFromPlayer;
}