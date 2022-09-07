using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy EnemyPrefab;
    [SerializeField]
    [Range(1, 100)]
    private int EnemiesToSpawn = 10;

    private NavMeshTriangulation Triangulation;

    private void Awake()
    {
        Triangulation = NavMesh.CalculateTriangulation();
    }

    private void Start()
    {
        for (int i = 0; i < EnemiesToSpawn; i++)
        {
            Enemy enemy = Instantiate(EnemyPrefab,
               Triangulation.vertices[Random.Range(0, Triangulation.vertices.Length)],
               Quaternion.identity
            );
            enemy.Movement.Triangulation = Triangulation;
            enemy.Health.OnDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath(Enemy Enemy)
    {
        StartCoroutine(DelayedDestroy(Enemy));
        Enemy.Movement.StopMoving();
        Enemy.Movement.enabled = false;
        Enemy.GetComponent<Animator>().SetTrigger("Death");
    }

    private IEnumerator DelayedDestroy(Enemy Enemy)
    {
        yield return new WaitForSeconds(3);

        Enemy.gameObject.SetActive(false);
    }
}