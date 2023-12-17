namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using UnityEngine;

    [TaskDescription("Exchange Items Between Inventories.")]
    [TaskCategory("Ultimate Inventory System")]
    [Tasks.HelpURL("TODO")]
    [TaskIcon("Assets/Behavior Designer/Integrations/UltimateInventorySystem/Editor/Icon.png")]
    public class ExchangeItemBetweenInventories : Action
    {
        [Tasks.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject m_TargetGameObject;
        [Tasks.Tooltip("The game object with the other inventory.")]
        public SharedGameObject m_OtherInventoryGameObject;
        [Tasks.Tooltip("The amount to check within the inventory.")]
        public SharedInt m_Amount;
        [Tasks.Tooltip("The item to check within the inventory.")]
        public SharedItemDefinition m_ItemDefinition;
        [Tasks.Tooltip("Check inherently if the item is within the item definition.")]
        public bool m_CheckInherently;
        [Tasks.Tooltip("Give or retrieve the item.")]
        public SharedBool m_GiveItem;

        // Cache the inventory component
        private Inventory m_Inventory;
        private GameObject m_PrevGameObject;
        
        private Inventory m_OtherInventory;
        private GameObject m_PrevOtherGameObject;
        
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

            var currentOtherGameObject = m_OtherInventoryGameObject.Value;
            if (currentOtherGameObject != m_PrevOtherGameObject) {
                m_OtherInventory = currentOtherGameObject.GetComponent<Inventory>();
                m_PrevOtherGameObject = currentOtherGameObject;
            }

        }

        /// <summary>
        /// Returns success if the item was added correctly otherwise it fails.
        /// </summary>
        /// <returns>The task status.</returns>
        public override TaskStatus OnUpdate()
        {
            if (m_Inventory == null) { return TaskStatus.Failure; }

            ItemInfo? itemInfo = null;

            var giver = m_GiveItem.Value ? m_Inventory : m_OtherInventory; 
            var receiver = m_GiveItem.Value ? m_OtherInventory : m_Inventory;
            
            itemInfo = giver.GetItemInfo(m_ItemDefinition.Value, m_CheckInherently);
            if(itemInfo.HasValue == false) { return TaskStatus.Failure; }

            var itemInfoValue = (ItemInfo)(Mathf.Min(m_Amount.Value, itemInfo.Value.Amount),itemInfo.Value);

            itemInfoValue = giver.RemoveItem(itemInfoValue);

            receiver.AddItem(itemInfoValue);
            
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
            m_OtherInventoryGameObject = null;
            m_GiveItem = false;
        }
    }
}