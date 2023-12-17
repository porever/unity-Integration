namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.Exchange;

    [System.Serializable]
    public class SharedCurrencyAmounts : SharedVariable<CurrencyAmounts>
    {
        public static implicit operator SharedCurrencyAmounts(CurrencyAmounts value) { return new SharedCurrencyAmounts { mValue = value }; }
    }
}