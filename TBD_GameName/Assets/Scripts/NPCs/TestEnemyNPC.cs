

using UnityEngine;

namespace NPC
{
    public class TestEnemyNPC : EnemyNPC
    {
        [SerializeField] private Transform target;

        private void Start()
        {
            stateManager.TransitionToState(new RoamState(this));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                stateManager.TransitionToState(new PursuitState(this, target));
            }
        }
    }
}
