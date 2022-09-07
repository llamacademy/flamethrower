using System.Collections;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyHealth : MonoBehaviour, IDamageable, IBurnable
{
    [SerializeField]
    private TextMeshPro HealthText;
    [SerializeField]
    private int _Health;
    public int Health { get => _Health; set => _Health = value; }

    [SerializeField]
    private bool _IsBurning;
    public bool IsBurning { get => _IsBurning; set => _IsBurning = value; }

    private Coroutine BurnCoroutine;

    public event DeathEvent OnDeath;
    public delegate void DeathEvent(Enemy Enemy);

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        HealthText.SetText(Health.ToString());
        if (Health <= 0)
        {
            Health = 0;
            OnDeath?.Invoke(GetComponent<Enemy>());
            StopBurning();
        }
    }

    public void StartBurning(int DamagePerSecond)
    {
        IsBurning = true;
        if (BurnCoroutine != null)
        {
            StopCoroutine(BurnCoroutine);
        }

        BurnCoroutine = StartCoroutine(Burn(DamagePerSecond));
    }

    private IEnumerator Burn(int DamagePerSecond)
    {
        float minTimeToDamage = 1f / DamagePerSecond;
        WaitForSeconds wait = new WaitForSeconds(minTimeToDamage);
        int damagePerTick = Mathf.FloorToInt(minTimeToDamage) + 1;
        
        TakeDamage(damagePerTick);
        while (IsBurning)
        {
            yield return wait;
            TakeDamage(damagePerTick);
        }
    }

    public void StopBurning()
    {
        IsBurning = false;
        if (BurnCoroutine != null)
        {
            StopCoroutine(BurnCoroutine);
        }
    }
}
