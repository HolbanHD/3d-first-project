using Interfaces;
using UnityEngine;

namespace NPC
{
    public class EnemyNPC : DamagableNPC
    {
        /// <summary>
        /// Base class for defining enemy npcs
        /// </summary>
        private EnemyNPCData attackingData; // Store the reference to AttackingNPCData
        protected override void Init()
        {
            base.Init();
            CheckCorrectNPCData();
        }

        private void CheckCorrectNPCData()
        {
            // Check if Data is of type EnemyNPCData and cache it in EnemyData
            if (Data is EnemyNPCData data)
            {
                attackingData = data;
                return;
            }
            Debug.LogError("Data is not of type EnemyNPCData!");
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
