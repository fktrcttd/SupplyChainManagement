using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TicketStore.Core
{
    public static class ReflectionHelper
    {
        public static IEnumerable<PropertyDescriptor> GetProperties<TCondition>(this Type @this) where TCondition : IPropertyCondition
        {
            var condition = Activator.CreateInstance(typeof(TCondition)) as IPropertyCondition;
            var properties = TypeDescriptor.GetProperties(@this)
                .OfType<PropertyDescriptor>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var property in properties.OrderBy(x =>
            {
                if (x.HasAttribute<PropertyOrderAttribute>())
                    return x.GetAttribute<PropertyOrderAttribute>().Order;
                if (x.ComponentType == typeof(Entity))
                    return 0;
                return int.MaxValue;
            }))
            {
                if (!condition.IsSatisfiedBy(property))
                    continue;

                yield return property;
            }
        }
        
        public static bool HasAttribute<TAttribute>(this PropertyDescriptor @this) where TAttribute : Attribute
        {
            if (@this == null)
                return false;
            return @this.Attributes[typeof(TAttribute)] != null;
        }
        
        public static TAttribute GetAttribute<TAttribute>(this Type @this, bool checkInheritance = false) where TAttribute : Attribute
        {
            if (checkInheritance)
                return @this.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;

            var attributes = TypeDescriptor.GetAttributes(@this);

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var attribute in attributes)
            {
                if (attribute is TAttribute)
                    return (TAttribute)attribute;
            }

            return null;
        }


        public static TAttribute GetAttribute<TAttribute>(this PropertyDescriptor @this) where TAttribute : Attribute
        {
            return (TAttribute)@this.Attributes[typeof(TAttribute)];
        }
        
        public static bool HasAttribute<TAttribute>(this Type @this, bool checkInheritance = false) where TAttribute : Attribute
        {
            if (checkInheritance)
                return @this.GetCustomAttributes(typeof(TAttribute), true).Any();

            return @this.CustomAttributes.Any(x => x.AttributeType == typeof(TAttribute));
        }

    }
}