
namespace Interfaces
{
    /// <summary>
    /// Interface for damagable entities
    /// </summary>
    public interface IDamagable
    {
        void TakeDamage(float damage);
        void Die();
    }
}
