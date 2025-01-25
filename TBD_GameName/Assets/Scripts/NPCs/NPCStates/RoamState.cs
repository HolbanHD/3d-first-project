using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    /// <summary>
    /// State in which NPC roams around a set area
    /// </summary>
    public class RoamState : NPCState
    {
        public RoamState(NPC npc) : base(npc) { } // RoamState constructor

        private Vector3 initialPosition;

        private float roamTimer;

        public override void Enter()
        {
            Debug.Log($"{npc.Data.NpcName} has entered RoamState");
            npc.Agent.isStopped = false;
            initialPosition = npc.transform.position;
            roamTimer = .3f; // Small initial value to start roaming
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            Roam();
        }

        /// <summary>
        /// Handle roaming behavior
        /// </summary>
        private void Roam()
        {
            if (npc.Agent.remainingDistance > npc.Agent.stoppingDistance) return;

            // If the agent has reached its destination
            if (roamTimer > 0)
            {
                roamTimer -= Time.deltaTime;
                if (roamTimer > 0) return; // Timer not done so exit method
            }

            // Timer is done, so start a new timer and set a new destination
            roamTimer = Random.Range(0, npc.Data.RoamDelayRange);
            SetRandomDestination();
        }

        private void SetRandomDestination()
        {
            Vector3 pos;
            if (TryGetRandomPosition(initialPosition, npc.Data.RoamRange, out pos))
            {
                npc.Agent.SetDestination(pos);
                RandomizeAgentSpeed();
            }
        }

        /// <summary>
        /// Randomize speed to create varation based on NPC's SO
        /// </summary>
        private void RandomizeAgentSpeed()
        {
            npc.Agent.speed = Random.Range(npc.Data.MinMovespeed, npc.Data.MaxMovespeed);
            npc.Agent.acceleration = Random.Range(npc.Data.MinAccelaration, npc.Data.MaxAccelaration);
        }

        /// <summary>
        /// Generate a random point within RoamRange,
        /// If point can find NavMesh within maxDistance, set result to found hit on NavMesh
        /// </summary>
        private bool TryGetRandomPosition(Vector3 center, float range, out Vector3 result)
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
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
    }
}
