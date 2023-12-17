using System;
using HutongGames.PlayMaker;
using Opsive.UltimateInventorySystem.Core;
using Opsive.UltimateInventorySystem.SaveSystem;
using Opsive.UltimateInventorySystem.UI.Panels;
using UnityEngine;

namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    [HutongGames.PlayMaker.Tooltip("Open, Close or Toggle a panel using its unique name.")]
    [ActionCategory("Ultimate Inventory System")]
    public class OpenClosePanel : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The Id for the Display Panel Manager.")]
        public FsmInt m_DisplayPanelManagerID;
        [HutongGames.PlayMaker.Tooltip("The unique name of the Panel.")]
        public FsmString m_PanelUniqueName;
        [HutongGames.PlayMaker.Tooltip("Toggle the panel on and off?")]
        public FsmBool m_Toggle;
        [HutongGames.PlayMaker.Tooltip("Close or Open the panel?")]
        public FsmBool m_Close;
        [HutongGames.PlayMaker.Tooltip("Close the selected panel if none with the name are found?")]
        public FsmBool m_CloseSelected;
        [HutongGames.PlayMaker.Tooltip("Event sent when the file was deleted.")]
        public FsmEvent m_SuccessEvent;

        /// <summary>
        /// The state has been started by the FSM.
        /// </summary>
        public override void OnEnter()
        {
            var displayManager = InventorySystemManager.GetDisplayPanelManager(Convert.ToUInt32(m_DisplayPanelManagerID.Value));

            if (displayManager == null) {
                Debug.LogWarning("The Display Panel Manager was not found.");
                Fsm.Event(m_SuccessEvent);
                Finish();
                return;
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
            
            Fsm.Event(m_SuccessEvent);
            Finish();
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            m_DisplayPanelManagerID = 1;
            m_PanelUniqueName = "MyPanel";
            m_Toggle = false;
            m_Close = false;
            m_CloseSelected = false;
            m_SuccessEvent = null;
        }
    }
}