using System;
using Opsive.UltimateInventorySystem.Core.DataStructures;
using Opsive.UltimateInventorySystem.ItemActions;
using UnityEngine;

namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    [Serializable]
    public class PlayMakerEventItemAction : ItemAction
    {
        [Tooltip("The item attribute name for the event name.")]
        [SerializeField] protected string m_AttributeEventName = "FSMEvent";
        [Tooltip("The default event name in the case none is found on the item attributes.")]
        [SerializeField] protected string m_DefaultEventName;
        [Tooltip("Adds the name of the item in the event name as <eventName>_<itemName>.")]
        [SerializeField] protected bool m_AppendItemName;
        [Tooltip("Log the event name before it is executed.")]
        [SerializeField] protected bool m_DebugEventName;
        
        protected override bool CanInvokeInternal(ItemInfo itemInfo, ItemUser itemUser)
        {
            return true;
        }

        protected override void InvokeActionInternal(ItemInfo itemInfo, ItemUser itemUser)
        {
     
            var fsm = GetPlayMakerFsm(itemInfo, itemUser);

            if (fsm == null) {
                Debug.LogError("The PlayMaker FSM could not be found on the ItemUser or the Inventory", itemUser);
                return;
            }
            
            if (itemInfo.Item == null || itemInfo.Item.TryGetAttributeValue(m_AttributeEventName, out string eventName) == false) {
                eventName = m_DefaultEventName;
            }

            if (m_AppendItemName) {
                eventName += "_"+itemInfo.Item?.name;
            }

            if (m_DebugEventName) {
                Debug.Log(eventName);
            }
            
            fsm.SendEvent(eventName);
        }

        protected virtual PlayMakerFSM GetPlayMakerFsm(ItemInfo itemInfo, ItemUser itemUser)
        {
            PlayMakerFSM fsm = null;

            if (itemUser != null) {
                fsm = itemUser.gameObject.GetComponent<PlayMakerFSM>();
                if (fsm != null){ return fsm; }
            }
            
            if (itemInfo.Inventory != null) {
                fsm = itemInfo.Inventory.gameObject.GetComponent<PlayMakerFSM>();
            }

            return fsm;
        }
    }
}