namespace NPC
{
    /// <summary>
    /// State manager for handling of state transitions and update
    /// </summary>
    public class NPCStateManager
    {
        private NPCState currentState;

        /// <summary>
        /// Exit current state if there is one, then enter new state
        /// </summary>
        /// <param name="newState">New state to transition to</param>
        public void TransitionToState(NPCState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        /// <summary>
        /// Call Update() in the current state
        /// </summary>
        public void StateUpdate()
        {
            currentState?.Update();
        }
    }
}
