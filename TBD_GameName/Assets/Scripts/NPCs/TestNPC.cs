using UnityEngine;

namespace NPC
{
    public class TestNPC : DamagableNPC
    {
        private NPCStateManager stateManager;

        protected override void Init()
        {
            base.Init();
            stateManager = new NPCStateManager();
            stateManager.TransitionToState(new IdleState(this));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                stateManager.TransitionToState(new RoamState(this));
            }
        }

        // Draw a gizmo in the Scene view to visualize the roam range
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Data.RoamRange);
        }
    }
}
