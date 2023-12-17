namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using HutongGames.PlayMaker;
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using Opsive.UltimateInventorySystem.DropsAndPickups;
    using UnityEngine;

    [HutongGames.PlayMaker.Tooltip("Drop an item.")]
    [ActionCategory("Ultimate Inventory System")]
    public class DropItemFromInventory : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public FsmOwnerDefault m_TargetGameObject;
        [HutongGames.PlayMaker.Tooltip("The Item Object Spawner is a component in the scene, use its ID to find it and spawn an Item Object.")]
        public FsmInt m_ItemObjectSpawnerID;
        [HutongGames.PlayMaker.Tooltip("The amount to remove from the inventory.")]
        public FsmInt m_Amount;
        [HutongGames.PlayMaker.Tooltip("The item to remove from the inventory.")]
        public ItemDefinition m_ItemDefinition;
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The item to check within the inventory.")]
        public FsmObject m_ItemDefinitionVariable;
        [HutongGames.PlayMaker.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public ItemCollectionPurpose m_ItemCollectionPurpose;
        [HutongGames.PlayMaker.Tooltip("The amount to check within the inventory.")]
        public FsmBool m_RemoveItemOnDrop;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was removed.")]
        public FsmEvent m_SuccessEvent;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was not removed.")]
        public FsmEvent m_FailEvent;
        [HutongGames.PlayMaker.Tooltip("Should the item be removed every frame?")]
        public bool m_EveryFrame;

        // Cache the inventory component
        private Inventory m_Inventory;
        private GameObject m_PrevGameObject;
        private ItemObjectSpawner m_ItemObjectSpawner;

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
            
            m_ItemObjectSpawner = InventorySystemManager.GetGlobal<ItemObjectSpawner>((uint)m_ItemObjectSpawnerID.Value);
        }

        /// <summary>
        /// The state has been started by the FSM.
        /// </summary>
        public override void OnEnter()
        {
            DropItem();
            if (!m_EveryFrame) {
                Finish();
            }
        }

        /// <summary>
        /// The FSM has updated.
        /// </summary>
        public override void OnUpdate()
        {
            DropItem();
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        public void DropItem()
        {
            if (m_Inventory == null) { Fsm.Event(m_FailEvent); return; }

            ItemInfo? itemInfo = null;
            
            var itemDefinition = m_ItemDefinitionVariable.Value as ItemDefinition;
            if (itemDefinition == null) {
                itemDefinition = m_ItemDefinition;
            }
            
            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionPurpose);
            if (itemCollection != null) {
                itemInfo = itemCollection.GetItemInfo(itemDefinition);
            } else {
                itemInfo = m_Inventory.GetItemInfo(itemDefinition);
            }

            if (itemInfo.HasValue == false) {
                Fsm.Event(m_FailEvent);
                return;
            }

            itemInfo = (m_Amount.Value, itemInfo.Value);
            if (m_RemoveItemOnDrop.Value) {
                itemInfo = m_Inventory.RemoveItem(itemInfo.Value);
            }
            m_ItemObjectSpawner.Spawn(itemInfo.Value, m_Inventory.transform.position);

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
        }
    }
}