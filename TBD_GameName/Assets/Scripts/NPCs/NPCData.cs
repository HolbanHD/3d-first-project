using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "New NPC", menuName = "NPC/NPCData")]
    public class NPCData : ScriptableObject
    {
        public string NpcName;
        public int MaxHealth;

        [Header("Movement Settings")]
        public float MinMovespeed;
        public float MaxMovespeed;
        public float MinAccelaration;
        public float MaxAccelaration;
        public float RoamRange;
        [Range(0f, 10f)]
        public float RoamDelayRange;
    }
}
