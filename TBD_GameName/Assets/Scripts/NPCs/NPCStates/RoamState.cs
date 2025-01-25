using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

namespace NPC
{
    /// <summary>
    /// State in which NPC roams around a set area
    /// </summary>
    public class RoamState : NPCState
    {
        public RoamState(NPC npc) : base(npc) { } // RoamState constructor

        private Vector3 initialPosition;

        private bool isMoving;

        public override void Enter()
        {
            Debug.Log($"{npc.Data.NpcName} has entered RoamState");
            npc.Agent.isStopped = false;
            initialPosition = npc.transform.position;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (npc.Agent.remainingDistance <= npc.Agent.stoppingDistance) // Done with path
            {
                Vector3 pos;
                if (RandomPosition(initialPosition, npc.Data.RoamRange, out pos))
                {
                    npc.Agent.SetDestination(pos);
                }
            }
        }

        /// <summary>
        /// Generate a random point within RoamRange,
        /// If point can find NavMesh within maxDistance, set result to found hit on NavMesh
        /// </summary>

        private bool RandomPosition(Vector3 center, float range, out Vector3 result)
        {
            // Generate a random point within RoamRange
            Vector3 randomPoint = new Vector3(
               center.x + Random.insideUnitSphere.x * range,
               center.y, // Keep the same Y value
               center.z + Random.insideUnitSphere.z * range);
            NavMeshHit hit;

            // If point can find NavMesh within maxDistance, set result to found hit on navMesh
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                npc.Agent.SetDestination(hit.position);
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
    }
}
