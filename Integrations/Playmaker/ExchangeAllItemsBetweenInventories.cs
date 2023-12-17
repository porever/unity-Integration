namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using System.Collections.Generic;
    using HutongGames.PlayMaker;
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using UnityEngine;

    [HutongGames.PlayMaker.Tooltip("Exchange Items Between Inventories.")]
    [ActionCategory("Ultimate Inventory System")]
    public class ExchangeAllItemsBetweenInventories : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public FsmOwnerDefault m_TargetGameObject;
        [HutongGames.PlayMaker.Tooltip("The game object with the other inventory.")]
        public FsmGameObject m_OtherInventoryGameObject;
        [HutongGames.PlayMaker.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public FsmString m_TargetItemCollectionName;
        [HutongGames.PlayMaker.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public FsmString m_OtherItemCollectionName;
        [HutongGames.PlayMaker.Tooltip("Give or retrieve the item.")]
        public FsmBool m_GiveItem;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was removed.")]
        public FsmEvent m_SuccessEvent;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was not removed.")]
        public FsmEvent m_FailEvent;
        [HutongGames.PlayMaker.Tooltip("Should the item be removed every frame?")]
        public bool m_EveryFrame;

        // Cache the inventory component
        private Inventory m_Inventory;
        private GameObject m_PrevGameObject;
        
        private Inventory m_OtherInventory;
        private GameObject m_PrevOtherGameObject;

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
            
            var currentOtherGameObject = m_OtherInventoryGameObject.Value;
            if (currentOtherGameObject != m_PrevOtherGameObject) {
                m_OtherInventory = currentOtherGameObject.GetComponent<Inventory>();
                m_PrevOtherGameObject = currentOtherGameObject;
            }
        }

        /// <summary>
        /// The state has been started by the FSM.
        /// </summary>
        public override void OnEnter()
        {
            ExchangeItem();
            if (!m_EveryFrame) {
                Finish();
            }
        }

        /// <summary>
        /// The FSM has updated.
        /// </summary>
        public override void OnUpdate()
        {
            ExchangeItem();
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        public void ExchangeItem()
        {
            if (m_Inventory == null || m_OtherInventory == null) { Fsm.Event(m_FailEvent); return; }

            var giver = m_GiveItem.Value ? m_Inventory : m_OtherInventory; 
            var receiver = m_GiveItem.Value ? m_OtherInventory : m_Inventory;

            var giverItemCollection = giver.GetItemCollection(m_TargetItemCollectionName.Value);
            var receiverItemCollection = receiver.GetItemCollection(m_OtherItemCollectionName.Value);
            if (giverItemCollection == null) {
                var items = giver.AllItemInfos;

                if (receiverItemCollection == null) {

                    for (int i = 0; i < items.Count; i++) {
                        var itemInfo = items[i];
                        var removed = giver.RemoveItem(itemInfo);
                        receiver.AddItem(removed);
                    }
                    
                } else {
                    for (int i = 0; i < items.Count; i++) {
                        var itemInfo = items[i];
                        var removed = giver.RemoveItem(itemInfo);
                        receiverItemCollection.AddItem(removed);
                    }
                }
            } else {
                var items = giverItemCollection.GetAllItemStacks();
                
                if (receiverItemCollection == null) {
                    for (int i = 0; i < items.Count; i++) {
                        var itemStack = items[i];
                        var removed = giverItemCollection.RemoveItem((ItemInfo)itemStack);
                        receiver.AddItem(removed);
                    }
                } else {
                    for (int i = 0; i < items.Count; i++) {
                        var itemStack = items[i];
                        var removed = giverItemCollection.RemoveItem((ItemInfo)itemStack);
                        receiverItemCollection.AddItem(removed);
                    }
                }
            }

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            m_TargetGameObject = null;
            m_TargetItemCollectionName = default;
            m_OtherItemCollectionName = default;
            m_SuccessEvent = null;
            m_FailEvent = null;
            m_OtherInventoryGameObject = null;
            m_GiveItem = false;
        }
    }
}