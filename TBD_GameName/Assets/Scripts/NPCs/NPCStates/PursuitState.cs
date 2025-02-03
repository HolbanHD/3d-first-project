using System.Collections;
using UnityEngine;

namespace NPC
{
    public class PursuitState : NPCState
    {
        private Transform pursuitTarget; // Target the NPC will pursue
        private bool isPursuiting;

        public PursuitState(EnemyNPC npc, Transform pursuitTarget) : base(npc) // PursuitState constructor
        {
            this.pursuitTarget = pursuitTarget;
        } 

        public override void Enter()
        {
            Debug.Log($"{npc.Data.NpcName} has entered PursuitState, targeting {pursuitTarget.name}");
            isPursuiting = true;
            npc.StartCoroutine(PursueTarget());
        }

        private IEnumerator PursueTarget()
        {
            while (isPursuiting)
            {
                npc.Agent.SetDestination(pursuitTarget.transform.position);
                yield return new WaitForSeconds(.2f);
            }
        }

        public override void Exit()
        {
            Debug.Log($"{npc.Data.NpcName} has exited PursuitState");
            npc.StopCoroutine(PursueTarget());
        }
    }
}
