/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using UnityEngine;

    [TaskDescription("Remove an amount of item from the inventory.")]
    [TaskCategory("Ultimate Inventory System")]
    [Tasks.HelpURL("TODO")]
    [TaskIcon("Assets/Behavior Designer/Integrations/UltimateInventorySystem/Editor/Icon.png")]
    public class RemoveItem : Action
    {
        [Tasks.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject m_TargetGameObject;
        [Tasks.Tooltip("The amount to remove from the inventory.")]
        public SharedInt m_Amount;
        [Tasks.Tooltip("The item to remove from the inventory.")]
        public SharedItemDefinition m_ItemDefinition;
        [Tasks.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public ItemCollectionPurpose m_ItemCollectionPurpose;

        // Cache the inventory component
        private Inventory m_Inventory;
        private GameObject m_PrevGameObject;

        /// <summary>
        /// Get the inventory on start.
        /// </summary>
        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(m_TargetGameObject.Value);
            if (currentGameObject != m_PrevGameObject) {
                m_Inventory = currentGameObject.GetComponent<Inventory>();
                m_PrevGameObject = currentGameObject;
            }
        }

        /// <summary>
        /// Returns success if the item was removed correctly otherwise it fails.
        /// </summary>
        /// <returns>The task status.</returns>
        public override TaskStatus OnUpdate()
        {
            if (m_Inventory == null) { return TaskStatus.Failure; }
            
            ItemInfo itemInfoRemoved;
            
            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionPurpose);
            if (itemCollection != null) {
                itemInfoRemoved = itemCollection.RemoveItem(m_ItemDefinition.Value, m_Amount.Value);
            } else {
                itemInfoRemoved = m_Inventory.RemoveItem(m_ItemDefinition.Value, m_Amount.Value);
            }
            
            return itemInfoRemoved.Amount != 0 ? TaskStatus.Success : TaskStatus.Failure;
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void OnReset()
        {
            m_TargetGameObject = null;
            m_Amount = 1;
            m_ItemDefinition = null;
            m_ItemCollectionPurpose = ItemCollectionPurpose.None;
        }
    }
}