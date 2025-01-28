using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    /// <summary>
    /// Base class for defining npcs
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class NPC : MonoBehaviour
    {
        [SerializeField] protected NPCData data;
        public NPCData Data => data; // public getter 
        public NavMeshAgent Agent { get; private set; }

        protected NPCStateManager stateManager;

        [Header("Movement Settings")]
        [SerializeField] private bool isMovingNPC = false;  // Flag to determine if NPC will move


        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            if (isMovingNPC)
                Agent = GetComponent<NavMeshAgent>();
            stateManager = new NPCStateManager();
        }
    }
}