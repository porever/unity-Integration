using System;
using Opsive.UltimateInventorySystem.Core;
using Opsive.UltimateInventorySystem.SaveSystem;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    [TaskDescription("Open, Close or Toggle a panel using its unique name.")]
    [TaskCategory("Ultimate Inventory System")]
    [Tasks.HelpURL("TODO")]
    [TaskIcon("Assets/Behavior Designer/Integrations/UltimateInventorySystem/Editor/Icon.png")]
    public class OpenClosePanel : Action
    {
        [Tasks.Tooltip("The Id for the Display Panel Manager.")]
        public SharedInt m_DisplayPanelManagerID;
        [Tasks.Tooltip("The unique name of the Panel.")]
        public SharedString m_PanelUniqueName;
        [Tasks.Tooltip("Toggle the panel on and off?")]
        public SharedBool m_Toggle;
        [Tasks.Tooltip("Close or Open the panel?")]
        public SharedBool m_Close;
        [Tasks.Tooltip("Close the selected panel if none with the name are found?")]
        public SharedBool m_CloseSelected;

        /// <summary>
        /// Returns success if the inventory has the item amount specified, otherwise it fails.
        /// </summary>
        /// <returns>The task status.</returns>
        public override TaskStatus OnUpdate()
        {
            var displayManager = InventorySystemManager.GetDisplayPanelManager(Convert.ToUInt32(m_DisplayPanelManagerID.Value));

            if (displayManager == null) {
                Debug.LogWarning("The Display Panel Manager was not found.");
                return TaskStatus.Failure;
            }
            
            if (m_Toggle.Value) {
                displayManager.TogglePanel(m_PanelUniqueName.Value);
            } else {

                if (m_Close.Value) {
                    var panel = displayManager.GetPanel(m_PanelUniqueName.Value);

                    if (panel != null) {
                        panel.Close();
                    } else if(m_CloseSelected.Value) {
                        displayManager.CloseSelectedPanel();
                    }
                    
                } else {
                    displayManager.OpenPanel(m_PanelUniqueName.Value);
                }
                
            }

            return TaskStatus.Success;
        }
    }
}