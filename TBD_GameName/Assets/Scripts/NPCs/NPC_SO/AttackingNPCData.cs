using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "New AttackingNPC", menuName = "NPC/AttackingNPCData")]
    public class AttackingNPCData : NPCData
    {
        [Header("Attack Settings")]

        [Tooltip("Minimum and maximum damage values (x = min, y = max).")]
        public Vector2 DamageRange;

        [Tooltip("Range from player the npc can attack from")]
        public float AttackRange;
    }
}
