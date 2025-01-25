

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
        public abstract void Exit();

        // Optional virtual method for updating states
        public virtual void Update()
        {
            // Default behavior is empty
        }
    }
}
