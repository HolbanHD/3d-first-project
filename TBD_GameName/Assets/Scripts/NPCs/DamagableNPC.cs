using Interfaces;
using UnityEngine;

namespace NPC
{
    public abstract class DamagableNPC : NPC, IDamagable
    {
        protected float currentHealth;

        protected override void Init()
        {
            base.Init();
            currentHealth = Data.MaxHealth;
        }

        public virtual void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            //add death logic
        }
    }
}
