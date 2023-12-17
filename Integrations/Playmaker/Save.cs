/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using HutongGames.PlayMaker;
    using Opsive.UltimateInventorySystem.SaveSystem;

    [HutongGames.PlayMaker.Tooltip("Save the game using the Inventory save system.")]
    [ActionCategory("Ultimate Inventory System")]
    public class Save : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The Save index specified which save file to overwrite.")]
        public FsmInt m_SaveIndex;
        [HutongGames.PlayMaker.Tooltip("Event sent when the game finished saving.")]
        public FsmEvent m_SuccessEvent;

        /// <summary>
        /// The state has been started by the FSM.
        /// </summary>
        public override void OnEnter()
        {
            SaveSystemManager.Save(m_SaveIndex.Value);
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