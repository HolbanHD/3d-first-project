using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    /// <summary>
    /// State in which NPC roams around a set area
    /// </summary>
    public class RoamState : NPCState, IUpdatableState
    {
        public RoamState(NPC npc) : base(npc) { } // RoamState constructor

        private Vector3 initialPosition;

        private float roamTimer;
        private float timeRoaming;
        private float maxRoamTime = 6;

        public override void Enter()
        {
            Debug.Log($"{npc.Data.NpcName} has entered RoamState");
            npc.Agent.isStopped = false;
            initialPosition = npc.transform.position;
            roamTimer = .3f; // Small initial value to start roaming
        }

        public override void Exit()
        {
            Debug.Log($"{npc.Data.NpcName} has exited RoamState");
        }

        public void Update()
        {
            Roam();
        }

        /// <summary>
        /// Handle roaming behavior
        /// </summary>
        private void Roam()
        {
            timeRoaming += Time.deltaTime; // Track how long NPC has been roaming

            if (HasExceededMaxRoamTime() || HasReachedDestination())
            {
                HandleRoamTimer();
            }
        }

        /// <summary>
        /// Checks if the NPC has exceeded the maximum allowed roam time.
        /// </summary>
        private bool HasExceededMaxRoamTime()
        {
            return timeRoaming >= maxRoamTime;
        }

        /// <summary>
        /// Checks if the NPC has reached its destination.
        /// </summary>
        private bool HasReachedDestination()
        {
            return npc.Agent.remainingDistance <= npc.Agent.stoppingDistance;
        }

        /// <summary>
        /// Handles the roam timer logic. If the timer is active, it counts down, and if it reaches zero, a new destination is set.
        /// </summary>
        private void HandleRoamTimer()
        {
            if (roamTimer > 0)
            {
                roamTimer -= Time.deltaTime;
                if (roamTimer > 0) return; // Timer still active, so exit method
            }

            StartNewRoamCycle();
        }

        /// <summary>
        /// Resets roaming state and sets a new random destination.
        /// </summary>
        private void StartNewRoamCycle()
        {
            timeRoaming = 0; // Reset time roaming
            roamTimer = Random.Range(npc.Data.RoamDelayRange.x, npc.Data.RoamDelayRange.y);
            if (!TrySetRandomDestination())
            {
                roamTimer += Time.deltaTime; // Try again next frame
            }
        }

        /// <summary>
        /// Attempts to set a new random destination. Returns true if successful, false otherwise.
        /// </summary>
        private bool TrySetRandomDestination()
        {
            Vector3 pos;
            if (TryGetRandomPosition(initialPosition, npc.Data.RoamRange, out pos))
            {
                npc.Agent.SetDestination(pos);
                RandomizeAgentSpeed();
                return true; // Successfully set a new destination
            }

            return false; // No valid position found
        }

        /// <summary>
        /// Randomize speed to create varation based on NPC's SO
        /// </summary>
        private void RandomizeAgentSpeed()
        {
            npc.Agent.speed = Random.Range(npc.Data.MovespeedRange.x, npc.Data.MovespeedRange.y);
            npc.Agent.acceleration = Random.Range(npc.Data.AccelarationRange.x, npc.Data.AccelarationRange.y);
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
