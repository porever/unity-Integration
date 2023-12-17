namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using HutongGames.PlayMaker;
    using Opsive.UltimateInventorySystem.Exchange;
    using UnityEngine;

    [HutongGames.PlayMaker.Tooltip("Get the amount of currency in the currencyOwner.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetCurrency : FsmStateAction
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
            GetAmount();
            if (!m_EveryFrame) {
                Finish();
            }
        }

        /// <summary>
        /// The FSM has updated.
        /// </summary>
        public override void OnUpdate()
        {
            GetAmount();
        }

        /// <summary>
        /// Does the currency comparison.
        /// </summary>
        public void GetAmount()
        {
            if (m_CurrencyOwner == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Amount.Value = m_CurrencyOwner.CurrencyAmount.GetAmountOf(m_Currency);
            
            if (m_Amount.Value > 0) {
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