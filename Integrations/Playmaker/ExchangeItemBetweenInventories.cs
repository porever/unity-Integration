namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using HutongGames.PlayMaker;
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using UnityEngine;

    [HutongGames.PlayMaker.Tooltip("Exchange Items Between Inventories.")]
    [ActionCategory("Ultimate Inventory System")]
    public class ExchangeItemBetweenInventories : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public FsmOwnerDefault m_TargetGameObject;
        [HutongGames.PlayMaker.Tooltip("The game object with the other inventory.")]
        public FsmGameObject m_OtherInventoryGameObject;
        [HutongGames.PlayMaker.Tooltip("The amount to remove from the inventory.")]
        public FsmInt m_Amount;
        [HutongGames.PlayMaker.Tooltip("The item to remove from the inventory.")]
        public ItemDefinition m_ItemDefinition;
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The item to check within the inventory.")]
        public FsmObject m_ItemDefinitionVariable;
        [HutongGames.PlayMaker.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public ItemCollectionPurpose m_ItemCollectionPurpose;
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
            if (m_Inventory == null) { Fsm.Event(m_FailEvent); return; }

            ItemInfo? itemInfo = null;
            
            var itemDefinition = m_ItemDefinitionVariable.Value as ItemDefinition;
            if (itemDefinition == null) {
                itemDefinition = m_ItemDefinition;
            }

            var giver = m_GiveItem.Value ? m_Inventory : m_OtherInventory; 
            var receiver = m_GiveItem.Value ? m_OtherInventory : m_Inventory;
            
            itemInfo = giver.GetItemInfo(itemDefinition);
            if (itemInfo.HasValue == false) {
                Fsm.Event(m_FailEvent);
                return;
            }

            var itemInfoValue = (ItemInfo)(Mathf.Min(m_Amount.Value, itemInfo.Value.Amount),itemInfo.Value);

            itemInfoValue = giver.RemoveItem(itemInfoValue);

            receiver.AddItem(itemInfoValue);

            Fsm.Event(m_SuccessEvent);
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
            m_OtherInventoryGameObject = null;
            m_GiveItem = false;
        }
    }
}