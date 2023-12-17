namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using Opsive.UltimateInventorySystem.DropsAndPickups;
    using Opsive.UltimateInventorySystem.ItemActions;
    using UnityEngine;

    [TaskDescription("Drop an item.")]
    [TaskCategory("Ultimate Inventory System")]
    [Tasks.HelpURL("TODO")]
    [TaskIcon("Assets/Behavior Designer/Integrations/UltimateInventorySystem/Editor/Icon.png")]
    public class DropItemFromInventory : Action
    {
        [Tasks.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject m_TargetGameObject;
        [Tasks.Tooltip("The Item Object Spawner is a component in the scene, use its ID to find it and spawn an Item Object.")]
        public SharedInt m_ItemObjectSpawnerID;
        [Tasks.Tooltip("The amount to check within the inventory.")]
        public SharedInt m_Amount;
        [Tasks.Tooltip("The item to check within the inventory.")]
        public SharedItemDefinition m_ItemDefinition;
        [Tasks.Tooltip("Check inherently if the item is within the item definition.")]
        public bool m_CheckInherently;
        [Tasks.Tooltip("The amount to check within the inventory.")]
        public SharedBool m_RemoveItemOnDrop;
        [Tasks.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public ItemCollectionPurpose m_ItemCollectionPurpose;

        // Cache the inventory component
        private Inventory m_Inventory;
        private GameObject m_PrevGameObject;
        private ItemObjectSpawner m_ItemObjectSpawner;
        
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

            m_ItemObjectSpawner = InventorySystemManager.GetGlobal<ItemObjectSpawner>((uint)m_ItemObjectSpawnerID.Value);
        }

        /// <summary>
        /// Returns success if the item was added correctly otherwise it fails.
        /// </summary>
        /// <returns>The task status.</returns>
        public override TaskStatus OnUpdate()
        {
            if (m_Inventory == null) { return TaskStatus.Failure; }

            ItemInfo? itemInfo = null;
            
            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionPurpose);
            if (itemCollection != null) {
                itemInfo = itemCollection.GetItemInfo(m_ItemDefinition.Value, m_CheckInherently);
            } else {
                itemInfo = m_Inventory.GetItemInfo(m_ItemDefinition.Value, m_CheckInherently);
            }

            if(itemInfo.HasValue == false) { return TaskStatus.Failure; }

            itemInfo = (m_Amount.Value, itemInfo.Value);
            if (m_RemoveItemOnDrop.Value) {
                itemInfo = m_Inventory.RemoveItem(itemInfo.Value);
            }
            m_ItemObjectSpawner.Spawn(itemInfo.Value, m_Inventory.transform.position);
            
            
            return TaskStatus.Success;
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