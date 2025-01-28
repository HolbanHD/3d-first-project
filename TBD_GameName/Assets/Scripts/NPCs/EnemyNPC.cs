using Interfaces;
using UnityEngine;

namespace NPC
{
    public class EnemyNPC : DamagableNPC
    {
        /// <summary>
        /// Base class for defining npcs that attack 
        /// </summary>
        private AttackingNPCData attackingData; // Store the reference to AttackingNPCData
        protected override void Init()
        {
            base.Init();

            // Check if Data is of type AttackingNPCData and store it in attackingData
            if (Data is AttackingNPCData data)
            {
                attackingData = data; // Store the casted data
            }
            else
            {
                Debug.LogError("Data is not of type AttackingNPCData!");
            }
        }

        protected virtual void Attack(IDamagable target)
        {
            // Randomize dmg value 
            float dmg = (int)Random.Range(attackingData.DamageRange.x, attackingData.DamageRange.y);

            target.TakeDamage(dmg);
        }

        public override void Die()
        {
            base.Die();
        }

    }
}
