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
            npc.Agent.isStopped = true;
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
