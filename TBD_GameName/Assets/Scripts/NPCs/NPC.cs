using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    /// <summary>
    /// Base class for defining npcs
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class NPC : MonoBehaviour, IDamagable
    {
        [SerializeField] protected NPCData data;

        protected float currentHealth;
        public NavMeshAgent Agent { get; private set; }

        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            Agent = GetComponent<NavMeshAgent>();
            currentHealth = data.MaxHealth;
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