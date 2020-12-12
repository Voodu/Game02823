using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Statistics
{
    [Serializable]
    public class Attribute
    {
        protected readonly List<AttributeModifier>               attributeModifiers;
        public readonly    ReadOnlyCollection<AttributeModifier> AttributeModifiers;
        public             float                                 BaseValue;

        protected bool  isDirty = true;
        protected float lastBaseValue;

        protected float _value;

        public virtual float Value
        {
            get
            {
                if (isDirty || (lastBaseValue != BaseValue))
                {
                    lastBaseValue = BaseValue;
                    _value        = ComputeFinalValue();
                    isDirty       = false;
                }

                return _value;
            }
        }

        public Attribute()
        {
            attributeModifiers = new List<AttributeModifier>();
            AttributeModifiers = attributeModifiers.AsReadOnly();
        }

        public Attribute(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void Add(AttributeModifier mod)
        {
            isDirty = true;
            attributeModifiers.Add(mod);
        }

        public virtual bool Remove(AttributeModifier mod)
        {
            if (attributeModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllFromSource(object source)
        {
            var numRemovals = attributeModifiers.RemoveAll(mod => mod.Source == source);

            if (numRemovals > 0)
            {
                isDirty = true;
                return true;
            }

            return false;
        }

        protected virtual int CompareOrder(AttributeModifier a, AttributeModifier b)
        {
            if (a.Order < b.Order)
            {
                return -1;
            }

            if (a.Order > b.Order)
            {
                return 1;
            }

            return 0; //if (a.Order == b.Order)
        }

        protected virtual float ComputeFinalValue()
        {
            var   finalValue    = BaseValue;
            float sumPercentAdd = 0;

            attributeModifiers.Sort(CompareOrder);

            for (var i = 0; i < attributeModifiers.Count; i++)
            {
                var mod = attributeModifiers[i];

                if (mod.Scaler == BonusScaler.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Scaler == BonusScaler.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if ((i + 1 >= attributeModifiers.Count) || (attributeModifiers[i + 1].Scaler != BonusScaler.PercentAdd))
                    {
                        finalValue    *= 1 + sumPercentAdd;
                        sumPercentAdd =  0;
                    }
                }
                else if (mod.Scaler == BonusScaler.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            // Workaround for float calculation errors, like displaying 12.00001 instead of 12
            return (float) Math.Round(finalValue, 4);
        }
    }
}