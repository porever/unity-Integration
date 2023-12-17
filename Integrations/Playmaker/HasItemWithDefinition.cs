/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using HutongGames.PlayMaker;
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using UnityEngine;

    [HutongGames.PlayMaker.Tooltip("Determines if an inventory has at least the amount of item specified.")]
    [ActionCategory("Ultimate Inventory System")]
    public class HasItemWithDefinition : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public FsmOwnerDefault m_TargetGameObject;
        [HutongGames.PlayMaker.Tooltip("The amount to check within the inventory.")]
        public FsmInt m_Amount;
        [HutongGames.PlayMaker.Tooltip("The item to check within the inventory.")]
        public ItemDefinition m_ItemDefinition;
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The item to check within the inventory.")]
        public FsmObject m_ItemDefinitionVariable;
        [HutongGames.PlayMaker.Tooltip("Check inherently if the item is equivalent.")]
        public bool m_CheckInherently;
        [HutongGames.PlayMaker.Tooltip("Check inherently if the item is equivalent.")]
        public bool m_CountStacks;
        [HutongGames.PlayMaker.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public ItemCollectionPurpose m_ItemCollectionPurpose;
        [HutongGames.PlayMaker.Tooltip("Event sent when the inventory has the item.")]
        public FsmEvent m_SuccessEvent;
        [HutongGames.PlayMaker.Tooltip("Event sent when the inventory does not have the item.")]
        public FsmEvent m_FailEvent;
        [HutongGames.PlayMaker.Tooltip("Should the item be compared every frame?")]
        public bool m_EveryFrame;

        // Cache the inventory component
        private Inventory m_Inventory;
        private GameObject m_PrevGameObject;

        /// <summary>
        /// Get the inventory on start.
        /// </summary>
        public override void Awake()
        {
            var currentGameObject = Fsm.GetOwnerDefaultTarget(m_TargetGameObject);
            if (currentGameObject != m_PrevGameObject) {
                m_Inventory = currentGameObject.GetComponent<Inventory>();
                m_PrevGameObject = currentGameObject;
            }
        }

        /// <summary>
        /// The state has been started by the FSM.
        /// </summary>
        public override void OnEnter()
        {
            DoComparison();
            if (!m_EveryFrame) {
                Finish();
            }
        }

        /// <summary>
        /// The FSM has updated.
        /// </summary>
        public override void OnUpdate()
        {
            DoComparison();
        }

        /// <summary>
        /// Does the item comparison.
        /// </summary>
        public void DoComparison()
        {
            if (m_Inventory == null) { return; }

            bool hasItem;
            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionPurpose);
            var itemDefinition = m_ItemDefinitionVariable.Value as ItemDefinition;
            if (itemDefinition == null) {
                itemDefinition = m_ItemDefinition;
            }
            if (itemCollection != null) {
                hasItem = itemCollection.HasItem((itemDefinition, m_Amount.Value), m_CheckInherently, m_CountStacks);
            } else {
                hasItem = m_Inventory.HasItem((m_Amount.Value, itemDefinition), m_CheckInherently, m_CountStacks);
            }

            if (hasItem) {
                Fsm.Event(m_SuccessEvent);
            } else {
                Fsm.Event(m_FailEvent);
            }
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            m_TargetGameObject = null;
            m_Amount = 1;
            m_ItemDefinition = null;
            m_ItemDefinitionVariable = null;
            m_CheckInherently = false;
            m_CountStacks = false;
            m_ItemCollectionPurpose = ItemCollectionPurpose.None;
            m_SuccessEvent = null;
            m_FailEvent = null;
        }
    }
}