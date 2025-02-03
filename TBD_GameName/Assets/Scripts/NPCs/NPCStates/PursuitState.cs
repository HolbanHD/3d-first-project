using System.Collections;
using UnityEngine;

namespace NPC
{
    public class PursuitState : NPCState
    {
        private Transform pursuitTarget; // Target the NPC will pursue
        private Coroutine pursuitCoroutine;
        private float stopDistance;
        private float setDestinationDelay;
        private bool isPursuing;

        public PursuitState(EnemyNPC npc, Transform pursuitTarget, float stopppingDistance) : base(npc) // PursuitState constructor
        {
            stopDistance = stopppingDistance;
            this.pursuitTarget = pursuitTarget;
        } 

        public override void Enter()
        {
            Debug.Log($"{npc.Data.NpcName} has entered PursuitState, targeting {pursuitTarget.name}");
            isPursuing = true;
            npc.Agent.stoppingDistance = stopDistance;
            pursuitCoroutine = npc.StartCoroutine(PursueTarget(setDestinationDelay));
        }

        /// <summary>
        /// Sets the destination of the NPC's agent to the target's position at regular intervals.
        /// </summary>
        private IEnumerator PursueTarget(float delay)
        {
            while (isPursuing)
            {
                npc.Agent.SetDestination(pursuitTarget.transform.position);
                yield return new WaitForSeconds(delay);
            }
        }

        public override void Exit()
        {
            Debug.Log($"{npc.Data.NpcName} has exited PursuitState");
            npc.StopCoroutine(pursuitCoroutine);
        }
    }
}
