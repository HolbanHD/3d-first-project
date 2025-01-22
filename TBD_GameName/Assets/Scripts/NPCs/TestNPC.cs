using UnityEngine;

namespace NPC
{
    public class TestNPC : NPC
    {
        private NPCStateManager stateManager;

        protected override void Init()
        {
            base.Init();
            stateManager = new NPCStateManager();
        }
    }
}
