using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "New NPC", menuName = "NPC/NPCData")]
    public class NPCData : ScriptableObject
    {
        public string Name;
        public int MaxHealth;
        public float Movespeed;
    }
}
