/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using HutongGames.PlayMaker;
    using Opsive.UltimateInventorySystem.SaveSystem;

    [HutongGames.PlayMaker.Tooltip("Delete a save file using the Inventory save system.")]
    [ActionCategory("Ultimate Inventory System")]
    public class DeleteSave : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The Save index of the file to delete.")]
        public FsmInt m_SaveIndex;
        [HutongGames.PlayMaker.Tooltip("Event sent when the file was deleted.")]
        public FsmEvent m_SuccessEvent;

        /// <summary>
        /// The state has been started by the FSM.
        /// </summary>
        public override void OnEnter()
        {
            SaveSystemManager.DeleteSave(m_SaveIndex.Value);
            Fsm.Event(m_SuccessEvent);
            Finish();
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            m_SaveIndex = 0;
            m_SuccessEvent = null;
        }
    }
}