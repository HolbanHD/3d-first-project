using UnityEngine;

namespace NPC
{
    public class TestNPC : DamagableNPC
    {
        protected override void Init()
        {
            base.Init();
            stateManager.TransitionToState(new IdleState(this));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                stateManager.TransitionToState(new RoamState(this));
            }
            stateManager.StateUpdate();
        }

        // Draw a gizmo in the Scene view to visualize the roam range
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Data.RoamRange);
        }
    }
}
