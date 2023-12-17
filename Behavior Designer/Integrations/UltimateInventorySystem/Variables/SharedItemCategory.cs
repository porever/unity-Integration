namespace BehaviorDesigner.Runtime.Tasks.UltimateInventorySystem
{
    using Opsive.UltimateInventorySystem.Core;

    [System.Serializable]
    public class SharedItemCategory : SharedVariable<ItemCategory>
    {
        public static implicit operator SharedItemCategory(ItemCategory value) { return new SharedItemCategory { mValue = value }; }
    }
}