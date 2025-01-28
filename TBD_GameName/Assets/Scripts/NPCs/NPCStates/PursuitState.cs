using UnityEngine;

namespace NPC
{
    public class PursuitState : NPCState
    {
        private Transform pursuitTarget; // Target the NPC will pursue
        private EnemyNPC enemyNPC;

        public PursuitState(EnemyNPC npc, Transform pursuitTarget) : base(npc) // PursuitState constructor
        {
            this.pursuitTarget = pursuitTarget;
            this.enemyNPC = npc;
        } 

        public override void Enter()
        {
            Debug.Log($"{npc.Data.NpcName} has entered PursuitState, targeting {pursuitTarget.name}");
            npc.Agent.isStopped = true;
        }

        public override void Exit()
        {
            Debug.Log($"{npc.Data.NpcName} has exited PursuitState");
        }
    }
}
