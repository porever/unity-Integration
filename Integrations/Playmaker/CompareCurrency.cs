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
    public class CompareCurrency : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public FsmOwnerDefault m_TargetGameObject;
        [HutongGames.PlayMaker.Tooltip("Choose how to compare the currency value.")]
        public CompareOperation m_Operation;
        [HutongGames.PlayMaker.Tooltip("The currency that should be compared.")]
        public Currency m_Currency;
        [HutongGames.PlayMaker.Tooltip("The amount of currency that should be compared.")]
        public FsmInt m_Amount;
        [HutongGames.PlayMaker.Tooltip("Event sent when the currency contains the amount specified.")]
        public FsmEvent m_SuccessEvent;
        [HutongGames.PlayMaker.Tooltip("Event sent when the currency does not contain the amount specified.")]
        public FsmEvent m_FailEvent;
        [HutongGames.PlayMaker.Tooltip("Should the currency be compared every frame?")]
        public bool m_EveryFrame;

        // Cache the inventory component
        private CurrencyOwner m_CurrencyOwner;
        private GameObject m_PrevGameObject;
        private CurrencyAmounts m_CurrencyAmounts;

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

            m_CurrencyAmounts = new CurrencyAmounts();
            m_CurrencyAmounts.Add(new CurrencyAmount(m_Currency, m_Amount.Value));
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
            if (m_CurrencyOwner == null) { return; }

            var equalTo = m_CurrencyOwner.CurrencyAmount.EquivalentTo(m_CurrencyAmounts);
            if (equalTo &&
               (m_Operation == CompareOperation.LessOrEqual || m_Operation == CompareOperation.GreaterOrEqual || m_Operation == CompareOperation.Equal)) {
                Fsm.Event(m_SuccessEvent);
            }

            var greaterOrEqualsTo = m_CurrencyOwner.CurrencyAmount.GreaterThanOrEqualTo(m_CurrencyAmounts);

            if (greaterOrEqualsTo && (m_Operation == CompareOperation.Greater || m_Operation == CompareOperation.GreaterOrEqual)) {
                Fsm.Event(m_SuccessEvent);
            } else if (m_Operation == CompareOperation.Less || m_Operation == CompareOperation.LessOrEqual) {
                Fsm.Event(m_SuccessEvent);
            }

            Fsm.Event(m_FailEvent);
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