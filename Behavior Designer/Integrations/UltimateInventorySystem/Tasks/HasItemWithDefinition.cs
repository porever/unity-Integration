/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using UnityEngine;

    [TaskDescription("Determines if an inventory has at least the amount of item specified.")]
    [TaskCategory("Ultimate Inventory System")]
    [Tasks.HelpURL("TODO")]
    [TaskIcon("Assets/Behavior Designer/Integrations/UltimateInventorySystem/Editor/Icon.png")]
    public class HasItemWithDefinition : Conditional
    {
        [Tasks.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject m_TargetGameObject;
        [Tasks.Tooltip("The amount to check within the inventory.")]
        public SharedInt m_Amount;
        [Tasks.Tooltip("The item to check within the inventory.")]
        public SharedItemDefinition m_ItemDefinition;
        [Tasks.Tooltip("Check inherently if the item is equivalent.")]
        public bool m_CheckInherently;
        [Tasks.Tooltip("Check inherently if the item is equivalent.")]
        public bool m_CountStacks;
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
        /// Returns success if the inventory has the item amount specified, otherwise it fails.
        /// </summary>
        /// <returns>The task status.</returns>
        public override TaskStatus OnUpdate()
        {
            if (m_Inventory == null) { return TaskStatus.Failure; }

            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionPurpose);
            if (itemCollection != null) {
                return itemCollection.HasItem((m_ItemDefinition.Value, m_Amount.Value), m_CheckInherently, m_CountStacks) ?
                    TaskStatus.Success : TaskStatus.Failure;
                
            }

            return m_Inventory.HasItem((m_Amount.Value,m_ItemDefinition.Value), m_CheckInherently, m_CountStacks) ?
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
            m_CheckInherently = false;
            m_CountStacks = false;
            m_ItemCollectionPurpose = ItemCollectionPurpose.None;
        }
    }
}