/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using Opsive.UltimateInventorySystem.ItemActions;
    using Opsive.UltimateInventorySystem.UI.DataContainers;
    using UnityEngine;

    [TaskDescription("Use an item action.")]
    [TaskCategory("Ultimate Inventory System")]
    [Tasks.HelpURL("TODO")]
    [TaskIcon("Assets/Behavior Designer/Integrations/UltimateInventorySystem/Editor/Icon.png")]
    public class UseItemFromInventory : Action
    {
        [Tasks.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject m_TargetGameObject;
        [Tasks.Tooltip("The Item Action Set with the actions to use.")]
        public CategoryItemActionSet m_ItemActionSet;
        [Tasks.Tooltip("The Item Action index to use.")]
        public SharedInt m_ActionIndex;
        [Tasks.Tooltip("Use all the amount in the Collection?.")]
        public SharedBool m_UseAllTheAmount;
        [Tasks.Tooltip("The amount to check within the inventory (only if Use All the amount is false).")]
        public SharedInt m_Amount;
        [Tasks.Tooltip("The item Definition to use.")]
        public SharedItemDefinition m_ItemDefinition;
        [Tasks.Tooltip("Check inherently if the item is within the item definition.")]
        public bool m_CheckInherently;
        [Tasks.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public SharedString m_ItemCollectionName;

        // Cache the inventory component
        private Inventory m_Inventory;
        private ItemUser m_ItemUser;
        private GameObject m_PrevGameObject;

        /// <summary>
        /// Get the inventory on start.
        /// </summary>
        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(m_TargetGameObject.Value);
            if (currentGameObject != m_PrevGameObject) {
                m_Inventory = currentGameObject.GetComponent<Inventory>();
                m_ItemUser = currentGameObject.GetComponent<ItemUser>();
                m_PrevGameObject = currentGameObject;
            }
        }

        /// <summary>
        /// Returns success if the item was added correctly otherwise it fails.
        /// </summary>
        /// <returns>The task status.</returns>
        public override TaskStatus OnUpdate()
        {
            if (m_Inventory == null) { return TaskStatus.Failure; }

            ItemInfo? itemInfo = null;
            
            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionName.Value);
            if (itemCollection != null) {
                itemInfo = itemCollection.GetItemInfo(m_ItemDefinition.Value, m_CheckInherently);
            } else {
                itemInfo = m_Inventory.GetItemInfo(m_ItemDefinition.Value, m_CheckInherently);
            }

            if(itemInfo.HasValue == false) { return TaskStatus.Failure; }
            
            var itemInfoToUse = m_UseAllTheAmount.Value ? itemInfo.Value : (m_Amount.Value, itemInfo.Value);

            var success = m_ItemActionSet.UseItemAction(itemInfoToUse,m_ItemUser,m_ActionIndex.Value);
            
            return success ?
                TaskStatus.Success : TaskStatus.Failure;
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void OnReset()
        {
            m_TargetGameObject = null;
            m_Amount = 1;
            m_ItemDefinition = null;
            m_ItemCollectionName = "MainCollection";
        }
    }
}