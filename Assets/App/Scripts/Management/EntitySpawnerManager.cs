using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitySpawnerManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] _Entity[] entitys;
    Dictionary<string, Queue<EntityMotor>> entitysDictionary = new();

    [System.Serializable]
    class _Entity
    {
        public string entityName;
        public EntityMotor entityPrefab;

        [Space(5)]
        public int instantiateAmount;

        [Space(10)]
        public AnimationCurve spawningCooldownPerTime;
        public Coroutine spawningCoroutine;
    }

    int entityCount;

    [Header("References")]
    [SerializeField] SSO_GameplayConfig ssoGameplayConfig;

    [Space(10)]
    // RSO
    [SerializeField] RSO_PlayerTransform rsoPlayerTransform;

    // RSF
    // RSP

    //[Header("Input")]
    [SerializeField] RSE_ReturnEntity rseReturnEntity;

    //[Header("Output")]

    private void OnEnable()
    {
        rseReturnEntity.AddListener(ReturnEntity);
    }
    private void OnDisable()
    {
        rseReturnEntity.RemoveListener(ReturnEntity);
    }

    private void Start()
    {
        foreach (_Entity _entity in entitys)
        {
            for (int i = 0; i < _entity.instantiateAmount; i++)
            {
                CreateEntity(_entity.entityName);
            }

            _entity.spawningCoroutine = 
                StartCoroutine(EntitySpawningCooldown(
                    _entity,
                    _entity.spawningCooldownPerTime.keys[0].time));
        }
    }

    IEnumerator EntitySpawningCooldown(_Entity _entity, float spawningCooldown)
    {
        yield return new WaitForSeconds(spawningCooldown);

        EntityMotor entity = GetEntity(_entity.entityName);

        Vector2 posOffset = Random.insideUnitCircle.normalized * ssoGameplayConfig.entitySpawningRange;
        entity.transform.position = 
            new Vector3(
                rsoPlayerTransform.Value.position.x + posOffset.x, 
                entity.GetYSpawnOffset(), 
                rsoPlayerTransform.Value.position.z + posOffset.y);

        Debug.DrawLine(rsoPlayerTransform.Value.position, rsoPlayerTransform.Value.position + new Vector3(
                rsoPlayerTransform.Value.position.x + posOffset.x,
                entity.GetYSpawnOffset(),
                rsoPlayerTransform.Value.position.z + posOffset.y), Color.blue, 1);

        print(posOffset);

        _entity.spawningCoroutine = StartCoroutine(EntitySpawningCooldown(_entity, _entity.spawningCooldownPerTime.Evaluate(Time.time)));
        
        entityCount++;
    }

    EntityMotor GetEntity(string entityName)
    {
        if (!entitysDictionary.ContainsKey(entityName) || entitysDictionary[entityName].Count <= 0)
            CreateEntity(entityName);

        EntityMotor entity = entitysDictionary[entityName].Dequeue();
        entity.gameObject.SetActive(true);
        return entity;
    }
    void ReturnEntity(EntityMotor entity)
    {
        entity.gameObject.SetActive(false);
        entitysDictionary[entity.GetName()].Enqueue(entity);

        entityCount--;
    }

    void CreateEntity(string entityName)
    {
        _Entity _entity = entitys.FirstOrDefault(x => x.entityName == entityName);

        if (_entity == null)
        {
            Debug.LogError($"There is no \"{entityName}\" in the Entity Manager");
            return;
        }

        if (!entitysDictionary.ContainsKey(entityName))
        {
            entitysDictionary.Add(entityName, new Queue<EntityMotor>());
        }

        EntityMotor entity = Instantiate(_entity.entityPrefab, transform);
        entity.SetName(entityName);
        entity.gameObject.SetActive(false);

        entitysDictionary[entityName].Enqueue(entity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rsoPlayerTransform.Value == null ? Vector3.zero : rsoPlayerTransform.Value.position, ssoGameplayConfig.entitySpawningRange);
    }
}