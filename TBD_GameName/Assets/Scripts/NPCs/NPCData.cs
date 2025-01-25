using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "New NPC", menuName = "NPC/NPCData")]
    public class NPCData : ScriptableObject
    {
        public string NpcName;
        public int MaxHealth;
        [Header("Movement Settings")]
        public float Movespeed;
        public float RoamRange;
    }
}
