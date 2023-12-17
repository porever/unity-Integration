/// ---------------------------------------------
/// Ultimate Inventory System
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateInventorySystem.Integrations.Playmaker
{
    using System;
    using HutongGames.PlayMaker;
    using HutongGames.PlayMaker.Actions;
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.AttributeSystem;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using Opsive.UltimateInventorySystem.Exchange;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueInt : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmInt m_Value;

        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<int>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<int>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();
            
            
            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = 0;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueFloat : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat m_Value;
        
        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<float>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<float>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = 0;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueBool : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmBool m_Value;
        
        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<bool>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<bool>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = false;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueColor : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmColor m_Value;
        
        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<Color>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<Color>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = Color.white;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueMaterial : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmMaterial m_Value;

        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<Material>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<Material>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = null;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueQuaternion : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmQuaternion m_Value;

        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<Quaternion>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<Quaternion>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = null;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueRect : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmRect m_Value;

        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<Rect>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<Rect>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = null;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueTexture : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmTexture m_Value;

        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<Texture>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<Texture>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = null;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueVector2 : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmVector2 m_Value;

        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<Vector2>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<Vector2>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = null;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueVector3 : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmVector3 m_Value;

        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<Vector3>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<Vector3>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = null;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueAnimationEnum : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmEnum m_Value;

        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<Enum>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<Enum>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = null;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueString : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmString m_Value;
        
        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<string>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<string>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = "";
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueObject : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmObject m_Value;
        
        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<Object>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<Object>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = null;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueGameObject : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        public FsmGameObject m_Value;
        
        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<GameObject>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {
                attribute.SetOverrideValue(m_Value.Value);
            } else {
                m_Value.Value = attribute.GetValue();
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<GameObject>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }
            
            m_Value.Value = attribute.GetValue();

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_Value = null;
        }
    }
    
    [HutongGames.PlayMaker.Tooltip("Get or Set an attribute value.")]
    [ActionCategory("Ultimate Inventory System")]
    public class GetSetItemAttributeValueCurrencyAmounts : GetSetItemAttributeValueBase
    {
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Object)]
        public FsmArray m_CurrenciesValue;
        [HutongGames.PlayMaker.Tooltip("The Value Which contains the value retrieved or contains the value to set.")]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Int)]
        public FsmArray m_AmountsValue;
        
        protected override void DoGetSetAttributeValue(Item item)
        {
            var attribute = item.GetAttribute<Attribute<CurrencyAmounts>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            if (m_SetValue.Value) {

                if (m_CurrenciesValue.Values.Length != m_AmountsValue.Values.Length) {
                    Fsm.Event(m_FailEvent);
                    return;
                }

                var array = new CurrencyAmount[m_CurrenciesValue.Values.Length];
                for (int i = 0; i < array.Length; i++) {
                    array[i] = new CurrencyAmount(m_CurrenciesValue.Values[i] as Currency, (int)m_AmountsValue.Values[i]);
                }

                var currencyAmounts = new CurrencyAmounts(array);
                
                attribute.SetOverrideValue(currencyAmounts);
            } else {
                var currencyAmounts = attribute.GetValue();

                m_CurrenciesValue.Resize(currencyAmounts.Count);
                m_AmountsValue.Resize(currencyAmounts.Count);

                for (int i = 0; i < currencyAmounts.Count; i++) {
                    m_CurrenciesValue.Set(i, currencyAmounts[i].Currency);
                    m_AmountsValue.Set(i, currencyAmounts[i].Amount);
                }
            }
            
            Fsm.Event(m_SuccessEvent);
        }

        protected override void DoGetAttributeValue(ItemDefinition itemDefinition)
        {
            var attribute = itemDefinition.GetAttribute<Attribute<CurrencyAmounts>>(m_AttributeName.Value);

            if (attribute == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            var currencyAmounts = attribute.GetValue();

            m_CurrenciesValue.Resize(currencyAmounts.Count);
            m_AmountsValue.Resize(currencyAmounts.Count);

            for (int i = 0; i < currencyAmounts.Count; i++) {
                m_CurrenciesValue.Set(i, currencyAmounts[i].Currency);
                m_AmountsValue.Set(i, currencyAmounts[i].Amount);
            }

            Fsm.Event(m_SuccessEvent);
        }

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_CurrenciesValue = null;
            m_AmountsValue = null;
        }
    }

    [HutongGames.PlayMaker.Tooltip("Add an amount of item to the inventory.")]
    [ActionCategory("Ultimate Inventory System")]
    public abstract class GetSetItemAttributeValueBase : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public FsmOwnerDefault m_TargetGameObject;
        [HutongGames.PlayMaker.Tooltip("Set the value to as the attribute value or Get the attribute value within the value variable.")]
        public FsmBool m_SetValue;
        [HutongGames.PlayMaker.Tooltip("Get the attribute valur on the Item Definition. Attribute values cannot be set on the definition at runtime")]
        public FsmBool m_GetValueOnDefinition;
        [HutongGames.PlayMaker.Tooltip("Get the attribute value on the default item of the Item Definition, it will not look in the Inventory.")]
        public FsmBool m_GetValueOnDefaultItem;
        [HutongGames.PlayMaker.Tooltip("The attribute name.")]
        public FsmString m_AttributeName;
        [HutongGames.PlayMaker.Tooltip("The item to check within the inventory.")]
        public ItemDefinition m_ItemDefinition;
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The item to check within the inventory.")]
        public FsmObject m_ItemDefinitionVariable;
        [HutongGames.PlayMaker.Tooltip("Should the item be looked inherently as a child of the ItemDefinition.")]
        public FsmBool m_CheckInherently;
        [HutongGames.PlayMaker.Tooltip("The Item collection to look for. If none the check will be done on the entire inventory.")]
        public ItemCollectionPurpose m_ItemCollectionPurpose;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was successfully added.")]
        public FsmEvent m_SuccessEvent;
        [HutongGames.PlayMaker.Tooltip("Event sent when the item was not successfully added.")]
        public FsmEvent m_FailEvent;
        [HutongGames.PlayMaker.Tooltip("Should the item be added every frame?")]
        public bool m_EveryFrame;

        // Cache the inventory component
        private Inventory m_Inventory;
        private GameObject m_PrevGameObject;

        /// <summary>
        /// Get the inventory on start.
        /// </summary>
        public override void Awake()
        {
            var currentGameObject = Fsm.GetOwnerDefaultTarget(m_TargetGameObject);
            if (currentGameObject != m_PrevGameObject) {
                m_Inventory = currentGameObject.GetComponent<Inventory>();
                m_PrevGameObject = currentGameObject;
            }
        }

        /// <summary>
        /// The state has been started by the FSM.
        /// </summary>
        public override void OnEnter()
        {
            DoGetSetAttributeValue();
            if (!m_EveryFrame) {
                Finish();
            }
        }

        /// <summary>
        /// The FSM has updated.
        /// </summary>
        public override void OnUpdate()
        {
            DoGetSetAttributeValue();
        }

        /// <summary>
        /// Adds the item to the inventory.
        /// </summary>
        protected virtual void DoGetSetAttributeValue()
        {
            var itemDefinition = m_ItemDefinitionVariable.Value as ItemDefinition;
            if (itemDefinition == null) {
                itemDefinition = m_ItemDefinition;
            }

            if (m_GetValueOnDefinition.Value) {
                DoGetAttributeValue(itemDefinition);
                return ;
            }else if (m_GetValueOnDefaultItem.Value) {
                DoGetSetAttributeValue(itemDefinition.DefaultItem);
            }
            
            if (m_Inventory == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            ItemInfo? itemInfoResult;
            var itemCollection = m_Inventory.GetItemCollection(m_ItemCollectionPurpose);
            if (itemCollection != null) {
                itemInfoResult = itemCollection.GetItemInfo(itemDefinition, m_CheckInherently.Value);
            } else {
                itemInfoResult = m_Inventory.GetItemInfo(itemDefinition, m_CheckInherently.Value);
            }

            if (itemInfoResult.HasValue == false || itemInfoResult.Value.Item == null) {
                Fsm.Event(m_FailEvent);
                return;
            }

            var item = itemInfoResult.Value.Item;

            DoGetSetAttributeValue(item);
        }

        abstract protected void DoGetSetAttributeValue(Item item);
        abstract protected void DoGetAttributeValue(ItemDefinition itemDefinition);

        /// <summary>
        /// Reset the public variables.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_TargetGameObject = null;
            m_SetValue = false;
            m_AttributeName = "MyAttribute";
            m_ItemDefinition = null;
            m_ItemDefinitionVariable = null;
            m_ItemCollectionPurpose = ItemCollectionPurpose.None;
            m_SuccessEvent = null;
            m_FailEvent = null;
        }
    }
}