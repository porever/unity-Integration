﻿/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using HutongGames.PlayMaker;
    using UnityEngine;

    [HutongGames.PlayMaker.Tooltip("Add an amount of item to the inventory.")]
    [ActionCategory("Ultimate Inventory System")]
    public class AddItem : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public FsmOwnerDefault m_TargetGameObject;
        [HutongGames.PlayMaker.Tooltip("The amount to check within the inventory.")]
        public FsmInt m_Amount;
        [HutongGames.PlayMaker.Tooltip("The item to check within the inventory.")]
        public ItemDefinition m_ItemDefinition;
        [HutongGames.PlayMaker.Tooltip("The item to check within the inventory.")]
        public FsmObject m_ItemDefinitionVariable;
        [HutongGames.PlayMaker.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public ItemCollectionPurpose m_ItemCollectionPurpose;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was successfully added.")]
        public FsmEvent m_SuccessEvent;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was not successfully added.")]
        public FsmEvent m_FailEvent;
        [HutongGames.PlayMaker.Tooltip("Should the item be added every frame?")]
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
            DoAddItem();
            if (!m_EveryFrame) {
                Finish();
            }
        }

        /// <summary>
        /// The FSM has updated.
        /// </summary>
        public override void OnUpdate()
        {
            DoAddItem();
        }

        /// <summary>
        /// Adds the item to the inventory.
        /// </summary>
        private void DoAddItem()
        {
            if (m_Inventory == null) {
                return;
            }

            ItemInfo itemInfo;
            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionPurpose);
            var itemDefinition = m_ItemDefinitionVariable.Value as ItemDefinition;
            if (itemDefinition == null) {
                itemDefinition = m_ItemDefinition;
            }
            
            if (itemCollection != null) {
                itemInfo = itemCollection.AddItem((ItemInfo)(m_Amount.Value, InventorySystemManager.CreateItem(itemDefinition)));
            } else {
                itemInfo = m_Inventory.AddItem((ItemInfo)(m_Amount.Value, InventorySystemManager.CreateItem(itemDefinition)));
            }

            if (itemInfo.Amount == m_Amount.Value) {
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
            m_ItemCollectionPurpose = ItemCollectionPurpose.None;
            m_SuccessEvent = null;
            m_FailEvent = null;
        }
    }
}