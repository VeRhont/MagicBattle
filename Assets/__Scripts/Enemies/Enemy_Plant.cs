using UnityEngine;
using System.Collections;

public class Enemy_Plant : Enemy
{
    [SerializeField] private GameObject _plantPrefab;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private int _attackCount;
    [SerializeField] private float _offset;

    private PlayerChaser playerChaser;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        playerChaser = new PlayerChaser(player, _offset);

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _attackCount; i++)
        {
            var position = playerChaser.GetPlayerPosition();

            yield return new WaitForSeconds(0.35f);

            var plant = Instantiate(_plantPrefab, position, Quaternion.identity);
            Destroy(plant, 1);

            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
        Die();
    }
}
