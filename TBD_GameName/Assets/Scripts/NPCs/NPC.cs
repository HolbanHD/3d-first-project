using UnityEngine;

namespace NPC
{
    public abstract class NPC : MonoBehaviour
    {
        [SerializeField] protected NPCData data;

        protected float currentHealth;

        protected virtual void Awake()
        {
            currentHealth = data.MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            //add death logic
        }
    }
}