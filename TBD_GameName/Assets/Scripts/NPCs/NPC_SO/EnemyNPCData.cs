using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "New EnemyNPC", menuName = "NPC/EnemyNPCData")]
    public class EnemyNPCData : NPCData
    {
        [Header("Attack Settings")]

        [Tooltip("Minimum and maximum damage values (x = min, y = max).")]
        public Vector2 DamageRange;

        [Tooltip("Range from player the npc can attack from")]
        public float AttackRange;
    }
}
