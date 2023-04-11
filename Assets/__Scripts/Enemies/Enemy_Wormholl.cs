using UnityEngine;

public class Enemy_Wormholl : Enemy
{
    [SerializeField] private float _destructionRadius;
    [SerializeField] private float _timeBetweenSpawn;

    private EnemyFactory _enemyFactory;

    private void OnEnable()
    {
        _enemyFactory = FindObjectOfType<EnemyFactory>();
        DestroyNearObjects();

        Invoke("SpawnHellball", _timeBetweenSpawn);
    }

    private void SpawnHellball()
    {
        _enemyFactory.CreateEnemy(EnemyType.Hellball, transform.position);
        Invoke("SpawnHellball", _timeBetweenSpawn);
    }

    private void DestroyNearObjects()
    {
        var circleHit = Physics2D.OverlapCircleAll(transform.position, _destructionRadius);

        foreach (var obj in circleHit)
        {
            if (obj.gameObject.CompareTag("Enviroment"))
            {
                Destroy(obj.gameObject);
            }
        }
    }
}
