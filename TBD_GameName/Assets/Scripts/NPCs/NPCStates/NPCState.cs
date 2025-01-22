
using UnityEngine;

namespace NPC
{
    /// <summary>
    /// Base class for defining npc states
    /// </summary>
    public abstract class NPCState
    {
        protected NPC npc; // Reference to the NPC

        public NPCState(NPC npc) // Base constructor for all NPCStates
        {
            this.npc = npc;
        }

        // Methods all states should implement
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();

        // Methods several states share
        protected void MoveTo(Vector3 position)
        {
            npc.Agent.SetDestination(position);
        }
    }
}
