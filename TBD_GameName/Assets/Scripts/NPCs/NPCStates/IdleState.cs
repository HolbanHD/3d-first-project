using UnityEngine;

namespace NPC
{
    /// <summary>
    /// State in which NPC stands in place idling
    /// </summary>
    public class IdleState : NPCState
    {
        public IdleState(NPC npc) : base(npc) { } // IdleState constructor

        public override void Enter()
        {
            Debug.Log($"{npc.Data.NpcName} has entered IdleState");
            npc.Agent.isStopped = true;
        }

        public override void Exit()
        {
            Debug.Log($"{npc.Data.NpcName} has exited IdleState");
        }
    }
}
