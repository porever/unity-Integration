/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.SaveSystem;

    [TaskDescription("Delete a save file using the Inventory save system.")]
    [TaskCategory("Ultimate Inventory System")]
    [Tasks.HelpURL("TODO")]
    [TaskIcon("Assets/Behavior Designer/Integrations/UltimateInventorySystem/Editor/Icon.png")]
    public class DeleteSave : Action
    {
        [Tasks.Tooltip("The Save index of the file to delete.")]
        public SharedInt m_SaveIndex;

        /// <summary>
        /// Returns success if the inventory has the item amount specified, otherwise it fails.
        /// </summary>
        /// <returns>The task status.</returns>
        public override TaskStatus OnUpdate()
        {
            SaveSystemManager.DeleteSave(m_SaveIndex.Value);

            return TaskStatus.Success;
        }
    }
}