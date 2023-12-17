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
    public class CompareItemAmount : Conditional
    {
        [Tasks.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject m_TargetGameObject;
        [Tasks.Tooltip("Choose how to compare the item amount.")]
        public Compare m_Compare;
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

            var currentAmount = 0;
            
            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionPurpose);
            if (itemCollection != null) {
                currentAmount = itemCollection.GetItemAmount(m_ItemDefinition.Value, m_CheckInherently, m_CountStacks);
            } else {
                currentAmount = m_Inventory.GetItemAmount(m_ItemDefinition.Value, m_CheckInherently, m_CountStacks);
            }

            if(currentAmount == m_Amount.Value && 
               (m_Compare == Compare.SmallerOrEqualsTo || m_Compare == Compare.GreaterOrEqualsTo || m_Compare == Compare.EqualsTo)) {
                return TaskStatus.Success;
            }

            if (currentAmount >= m_Amount.Value && (m_Compare == Compare.GreaterThan || m_Compare == Compare.GreaterOrEqualsTo) ) {
                return TaskStatus.Success;
            }else if ( m_Compare ==Compare.SmallerThan || m_Compare == Compare.SmallerOrEqualsTo) {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
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