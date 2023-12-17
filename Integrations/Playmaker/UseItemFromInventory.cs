/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using HutongGames.PlayMaker;
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using Opsive.UltimateInventorySystem.ItemActions;
    using Opsive.UltimateInventorySystem.UI.DataContainers;
    using UnityEngine;

    [HutongGames.PlayMaker.Tooltip("Use an item action.")]
    [ActionCategory("Ultimate Inventory System")]
    public class UseItemFromInventory : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public FsmOwnerDefault m_TargetGameObject;
        [HutongGames.PlayMaker.Tooltip("The amount to check within the inventory.")]
        public CategoryItemActionSet m_ItemActionSet;
        [HutongGames.PlayMaker.Tooltip("The amount to check within the inventory.")]
        public FsmInt m_ActionIndex;
        [HutongGames.PlayMaker.Tooltip("The amount to check within the inventory.")]
        public FsmInt m_Amount;
        [HutongGames.PlayMaker.Tooltip("The item to check within the inventory.")]
        public ItemDefinition m_ItemDefinition;
        [HutongGames.PlayMaker.Tooltip("Check inherently if the item is within the item definition.")]
        public bool m_CheckInherently;
        [HutongGames.PlayMaker.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public ItemCollectionPurpose m_ItemCollectionPurpose;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was used.")]
        public FsmEvent m_SuccessEvent;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was not used.")]
        public FsmEvent m_FailEvent;
        [HutongGames.PlayMaker.Tooltip("Should the item be used every frame?")]
        public bool m_EveryFrame;

        // Cache the inventory component
        private Inventory m_Inventory;
        private ItemUser m_ItemUser;
        private GameObject m_PrevGameObject;

        /// <summary>
        /// Get the inventory on start.
        /// </summary>
        public override void Awake()
        {
            var currentGameObject = Fsm.GetOwnerDefaultTarget(m_TargetGameObject);
            if (currentGameObject != m_PrevGameObject) {
                m_Inventory = currentGameObject.GetComponent<Inventory>();
                m_ItemUser = currentGameObject.GetComponent<ItemUser>();
                m_PrevGameObject = currentGameObject;
            }
        }

        /// <summary>
        /// The state has been started by the FSM.
        /// </summary>
        public override void OnEnter()
        {
            DoUse();
            if (!m_EveryFrame) {
                Finish();
            }
        }

        /// <summary>
        /// The FSM has updated.
        /// </summary>
        public override void OnUpdate()
        {
            DoUse();
        }

        /// <summary>
        /// Uses the item.
        /// </summary>
        public void DoUse()
        {
            if (m_Inventory == null) { Fsm.Event(m_FailEvent); return; }

            ItemInfo? itemInfo = null;
            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionPurpose);
            if (itemCollection != null) {
                itemInfo = itemCollection.GetItemInfo(m_ItemDefinition, m_CheckInherently);
            } else {
                itemInfo = m_Inventory.GetItemInfo(m_ItemDefinition, m_CheckInherently);
            }
            if (itemInfo.HasValue == false) { Fsm.Event(m_FailEvent); return; }

            var success = m_ItemActionSet.UseItemAction((m_Amount.Value, itemInfo.Value), m_ItemUser, m_ActionIndex.Value);
            if (success) {
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
            m_ItemCollectionPurpose = ItemCollectionPurpose.None;
            m_SuccessEvent = null;
            m_FailEvent = null;
        }
    }
}