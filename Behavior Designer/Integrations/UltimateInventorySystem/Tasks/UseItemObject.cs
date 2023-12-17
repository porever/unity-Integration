/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.Shared.Events;
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Equipping;
    using Opsive.UltimateInventorySystem.ItemActions;
    using UnityEngine;

    [TaskDescription("Use an item object from the equipper.")]
    [TaskCategory("Ultimate Inventory System")]
    [Tasks.HelpURL("TODO")]
    [TaskIcon("Assets/Behavior Designer/Integrations/UltimateInventorySystem/Editor/Icon.png")]
    public class UseItemObject : Action
    {
        [Tasks.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject m_TargetGameObject;
        [Tasks.Tooltip("The item object slot index in the equipper.")]
        public SharedInt m_ItemObjectSlotIndex;
        [Tasks.Tooltip("The amount to check within the inventory.")]
        public SharedInt m_ActionIndex;

        // Cache the inventory component
        private UsableEquippedItemsHandler m_UsableEquippedItemsHandler;
        private GameObject m_PrevGameObject;

        /// <summary>
        /// Get the inventory on start.
        /// </summary>
        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(m_TargetGameObject.Value);
            if (currentGameObject != m_PrevGameObject) {
                m_UsableEquippedItemsHandler = currentGameObject.GetComponent<UsableEquippedItemsHandler>();
                m_PrevGameObject = currentGameObject;
            }
        }

        /// <summary>
        /// Returns success if the item was added correctly otherwise it fails.
        /// </summary>
        /// <returns>The task status.</returns>
        public override TaskStatus OnUpdate()
        {
            if (m_UsableEquippedItemsHandler == null) { return TaskStatus.Failure; }
            
            m_UsableEquippedItemsHandler.UseItem(m_ItemObjectSlotIndex.Value,m_ActionIndex.Value);

            return TaskStatus.Success;
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void OnReset()
        {
            m_TargetGameObject = null;
            m_ItemObjectSlotIndex = 0;
            m_ActionIndex = 0;
        }
    }
}