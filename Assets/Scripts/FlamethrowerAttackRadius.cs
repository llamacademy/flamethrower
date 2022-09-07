using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public class FlamethrowerAttackRadius : MonoBehaviour
{
    public delegate void EnemyEnteredEvent(Enemy Enemy);
    public delegate void EnemyExitedEvent(Enemy Enemy);

    public event EnemyEnteredEvent OnEnemyEnter;
    public event EnemyEnteredEvent OnEnemyExit;

    private List<Enemy> EnemiesInRadius = new List<Enemy>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            EnemiesInRadius.Add(enemy);
            OnEnemyEnter?.Invoke(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            EnemiesInRadius.Remove(enemy);
            OnEnemyExit?.Invoke(enemy);
        }
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in EnemiesInRadius)
        {
            OnEnemyExit?.Invoke(enemy);
        }

        EnemiesInRadius.Clear();
    }
}
 