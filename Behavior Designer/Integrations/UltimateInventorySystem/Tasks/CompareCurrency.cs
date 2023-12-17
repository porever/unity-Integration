/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.Exchange;
    using UnityEngine;

    [TaskDescription("Determines if an currencyOwner has at least the amount of currency specified.")]
    [TaskCategory("Ultimate Inventory System")]
    [Tasks.HelpURL("TODO")]
    [TaskIcon("Assets/Behavior Designer/Integrations/UltimateInventorySystem/Editor/Icon.png")]
    public class CompareCurrency : Conditional
    {
        [Tasks.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject m_TargetGameObject;
        [Tasks.Tooltip("Choose how to compare the currency value.")]
        public Compare m_Compare;
        [Tasks.Tooltip("The currency amounts to check within the currency owner.")]
        public SharedCurrencyAmounts m_CurrencyAmounts;
        
        // Cache the inventory component
        private CurrencyOwner m_CurrencyOwner;
        private GameObject m_PrevGameObject;

        /// <summary>
        /// Get the inventory on start.
        /// </summary>
        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(m_TargetGameObject.Value);
            if (currentGameObject != m_PrevGameObject) {
                m_CurrencyOwner = currentGameObject.GetComponent<CurrencyOwner>();
                m_PrevGameObject = currentGameObject;
            }
        }
        
        /// <summary>
        /// Returns success if the inventory has the item amount specified, otherwise it fails.
        /// </summary>
        /// <returns>The task status.</returns>
        public override TaskStatus OnUpdate()
        {
            if (m_CurrencyOwner == null) { return TaskStatus.Failure; }

            var equalTo = m_CurrencyOwner.CurrencyAmount.EquivalentTo(m_CurrencyAmounts.Value);
            
            if(equalTo && 
               (m_Compare == Compare.SmallerOrEqualsTo || m_Compare == Compare.GreaterOrEqualsTo || m_Compare == Compare.EqualsTo)) {
                return TaskStatus.Success;
            }
            
            var greaterOrEqualsTo = m_CurrencyOwner.CurrencyAmount.GreaterThanOrEqualTo(m_CurrencyAmounts.Value);

            if (greaterOrEqualsTo && (m_Compare == Compare.GreaterThan || m_Compare == Compare.GreaterOrEqualsTo) ) {
                return TaskStatus.Success;
            }else if ( m_Compare ==Compare.SmallerThan || m_Compare == Compare.SmallerOrEqualsTo) {
                return TaskStatus.Success;
            }
            
            return TaskStatus.Failure;
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void OnReset()
        {
            m_TargetGameObject = null;
            m_CurrencyAmounts = null;
        }
    }
}