using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Inventory;

namespace Statistics
{
    [Serializable]
    public class Attribute
    {
        protected readonly List<AttributeModifier>               modifiers;
        public readonly    ReadOnlyCollection<AttributeModifier> attributeModifiers;
        public             float                                 baseValue;

        protected float lastBaseValue;

        protected float value;

        public virtual float Value => value;

        public Attribute()
        {
            modifiers          = new List<AttributeModifier>();
            attributeModifiers = modifiers.AsReadOnly();
        }

        public Attribute(float baseValue) : this()
        {
            this.baseValue = baseValue;
            value          = this.baseValue;
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        private void UpdateValue()
        {
            lastBaseValue = baseValue;
            value         = ComputeFinalValue();
            OnValueChanged(value);
        }

        public virtual void Add(AttributeModifier mod)
        {
            modifiers.Add(mod);
            UpdateValue();
        }

        public virtual bool Remove(AttributeModifier mod)
        {
            if (modifiers.Remove(mod))
            {
                UpdateValue();
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllFromSource(object source)
        {
            var numRemovals = modifiers.RemoveAll(mod => mod.source == source);

            if (numRemovals > 0)
            {
                UpdateValue();
                return true;
            }

            return false;
        }

        protected virtual int CompareOrder(AttributeModifier a, AttributeModifier b)
        {
            if (a.order < b.order)
            {
                return -1;
            }

            if (a.order > b.order)
            {
                return 1;
            }

            return 0; //if (a.Order == b.Order)
        }

        private void OnValueChanged(float newValue)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(newValue));
        }

        protected virtual float ComputeFinalValue()
        {
            var   finalValue    = baseValue;
            float sumPercentAdd = 0;

            modifiers.Sort(CompareOrder);

            for (var i = 0; i < modifiers.Count; i++)
            {
                var mod = modifiers[i];

                switch (mod.scaler)
                {
                    case BonusScaler.Flat:
                        finalValue += mod.value;
                        break;
                    case BonusScaler.PercentAdd:
                    {
                        sumPercentAdd += mod.value;

                        if ((i + 1 >= modifiers.Count) || (modifiers[i + 1].scaler != BonusScaler.PercentAdd))
                        {
                            finalValue    *= 1 + sumPercentAdd;
                            sumPercentAdd =  0;
                        }

                        break;
                    }

                    case BonusScaler.PercentMult:
                        finalValue *= 1 + mod.value;
                        break;
                }
            }

            // Workaround for float calculation errors, like displaying 12.00001 instead of 12
            return (float) Math.Round(finalValue, 4);
        }
    }
}