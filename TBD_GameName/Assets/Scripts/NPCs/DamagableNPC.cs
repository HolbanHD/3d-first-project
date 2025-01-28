using Interfaces;

namespace NPC
{
    /// <summary>
    /// Base class for defining npcs with the IDamagable interface
    /// </summary>
    public abstract class DamagableNPC : NPC, IDamagable
    {
        protected float currentHealth;

        protected override void Init()
        {
            base.Init();
            currentHealth = Data.MaxHealth;
        }
        
        /// <summary>
        /// Reduce currentHealth by damage param and check if it's at or below 0, if it is call Die()
        /// </summary>
        public virtual void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Called when currentHealth is at or below 0
        /// </summary>
        public virtual void Die()
        {
            //add death logic
        }
    }
}
