using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class NPC : MonoBehaviour, IDamagable
    {
        [SerializeField] protected NPCData data;

        protected float currentHealth;
        protected NavMeshAgent agent;

        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            agent = GetComponent<NavMeshAgent>();
            currentHealth = data.MaxHealth;
        }

        public virtual void MoveTo(Vector3 position)
        {
            agent.SetDestination(position);
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