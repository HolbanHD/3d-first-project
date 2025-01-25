using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class RoamState : NPCState
    {
        public RoamState(NPC npc) : base(npc) { }// RoamState constructor

        private Vector3 initialPosition;

        public override void Enter()
        {
            Debug.Log($"{npc.Data.NpcName} has entered RoamState");
            npc.Agent.isStopped = false;
            initialPosition = npc.transform.position;
            npc.StartCoroutine(Roam());
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        private IEnumerator Roam()
        {
            while (true)
            {
                MoveToRandomPoint();
                yield return new WaitForSeconds(3);
            }
            yield return null;
        }

        private void MoveToRandomPoint()
        {
            // Generate a random point within range
            Vector3 randomPoint = new Vector3(
                initialPosition.x + Random.insideUnitSphere.x * npc.Data.RoamRange,
                initialPosition.y, // Keep the same Y value
                initialPosition.z + Random.insideUnitSphere.z * npc.Data.RoamRange);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                npc.Agent.SetDestination(hit.position);
            }
            else
            {
                Debug.LogError("No valid position found on NavMesh!");
            }
            Debug.Log(randomPoint);
        }
    }
}
