namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.Core;

    [System.Serializable]
    public class SharedItemDefinition : SharedVariable<ItemDefinition>
    {
        public static implicit operator SharedItemDefinition(ItemDefinition value) { return new SharedItemDefinition { mValue = value }; }
    }
}