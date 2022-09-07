public interface IDamageable 
{
    public int Health { get; set; }
    public void TakeDamage(int Damage);
}
