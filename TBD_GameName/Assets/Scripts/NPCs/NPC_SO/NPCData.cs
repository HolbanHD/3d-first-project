using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "New NPC", menuName = "NPC/NPCData")]
    public class NPCData : ScriptableObject
    {
        public string NpcName;
        public int MaxHealth;

        [Header("Movement Settings")]

        [Tooltip("Minimum and maximum speed values (x = min, y = max).")]
        public Vector2 MovespeedRange;

        [Tooltip("Minimum and maximum accelaration values (x = min, y = max).")]
        public Vector2 AccelarationRange;

        [Tooltip("Minimum and maximum delay between agent movement values (x = min, y = max).")]
        public Vector2 RoamDelayRange;

        [Tooltip("Circular range in which the npc can roam from it's starting position \n(range should not encompass areas without NavMesh)")]
        public float RoamRange;
    }
}
