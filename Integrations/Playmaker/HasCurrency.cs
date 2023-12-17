/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using HutongGames.PlayMaker;
    using Opsive.UltimateInventorySystem.Exchange;
    using UnityEngine;

    [HutongGames.PlayMaker.Tooltip("Determines if an currencyOwner has at least the amount of currency specified.")]
    [ActionCategory("Ultimate Inventory System")]
    public class HasCurrency : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public FsmOwnerDefault m_TargetGameObject;
        [HutongGames.PlayMaker.Tooltip("The currency that should be checked.")]
        public Currency m_Currency;
        [HutongGames.PlayMaker.Tooltip("The amount of currency that should be checked.")]
        public FsmInt m_Amount;
        [HutongGames.PlayMaker.Tooltip("Event sent when the owner has the currency.")]
        public FsmEvent m_SuccessEvent;
        [HutongGames.PlayMaker.Tooltip("Event sent when the owner does not have the currency.")]
        public FsmEvent m_FailEvent;
        [HutongGames.PlayMaker.Tooltip("Should the currency be compared every frame?")]
        public bool m_EveryFrame;

        // Cache the inventory component
        private CurrencyOwner m_CurrencyOwner;
        private GameObject m_PrevGameObject;

        /// <summary>
        /// Get the inventory on start.
        /// </summary>
        public override void Awake()
        {
            var currentGameObject = Fsm.GetOwnerDefaultTarget(m_TargetGameObject);
            if (currentGameObject != m_PrevGameObject) {
                m_CurrencyOwner = currentGameObject.GetComponent<CurrencyOwner>();
                m_PrevGameObject = currentGameObject;
            }
        }

        /// <summary>
        /// The state has been started by the FSM.
        /// </summary>
        public override void OnEnter()
        {
            DoComparison();
            if (!m_EveryFrame) {
                Finish();
            }
        }

        /// <summary>
        /// The FSM has updated.
        /// </summary>
        public override void OnUpdate()
        {
            DoComparison();
        }

        /// <summary>
        /// Does the currency comparison.
        /// </summary>
        public void DoComparison()
        {
            var success = m_CurrencyOwner != null && m_CurrencyOwner.CurrencyAmount.HasCurrency(m_Currency, m_Amount.Value);
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
            m_Currency = null;
            m_Amount = 0;
            m_SuccessEvent = null;
            m_FailEvent = null;
        }
    }
}